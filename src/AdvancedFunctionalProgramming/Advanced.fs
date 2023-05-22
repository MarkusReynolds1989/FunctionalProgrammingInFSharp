namespace AdvancedFunctionalProgramming

open System
open System.Text.Json
open System.Xml

module Recursion =
    // Can't use this for the most part in C# because you will run out of stack space.
    // Tail recursion is when you set the last branch to be the recursion, if you have the program set to release
    // it will create heap space to hold the frames instead of allocating on the stack, allowing for
    // recursion in the place of loops.

    // Given a list, it will count the elements of the list and then return the count.
    let rec count input =
        match input with
        | [] -> 0
        | _ :: tail -> 1 + (count tail)

    // This will recurse through the collection and apply the accumulator to the "head" of the list.
    // It takes the result of the application and uses it as an argument for the next tail call.
    let rec reduce inputCollection reducer accumulator =
        match inputCollection with
        | [] -> accumulator
        | head :: tail -> reduce tail reducer (reducer accumulator head) // The recursion happens in the "tail" position of the function.

    // We could fix this with a recursive type.
    // let x = count seq[1;2;3;4]
    let x = count [ 1; 2; 3; 4 ]
    let q = count []

    let y = reduce [ 1; 2; 3; 4 ] (+) 0
    let z = reduce [ 1; 2; 3; 4 ] (fun x y -> x * y) 1

module RecursiveDataTypes =
    // I have a DU of items and I need to determine which function to call, so I build a helper
    // function that returns a function and maybe even data depending on
    // which branch gets hit.

    // Classic example of when we have some different shapes and have arithmetic to do on those shapes.
    // If you are familiar with OOP this is how we can have polymorphism in a functional programming language.
    // Now we have a type that represents a few different sub types and we can determine what to do with
    // any type we get using pattern matching.
    type Shape =
        | Circle of float
        | Square of float
        | Rectangle of float * float

    let getArea (shape: Shape) =
        match shape with
        | Circle radius -> Math.PI * (radius ** 2)
        | Square side -> side ** 2
        | Rectangle(length, width) -> length * width

    // Remember the issue above? We could solve it by making a "Collections" DU
    type CollectionItem<'a> =
        | ArrayType of array<'a>
        | SeqType of seq<'a>
        | ListType of list<'a>

    let rec arrayCount input =
        match input with
        | [||] -> 0
        | tail -> 1 + arrayCount (Array.skip 1 tail)

    let rec seqCount (input: seq<'a>) =
        match input with
        | _ when Seq.isEmpty input -> 0
        | tail -> 1 + seqCount (Seq.skip 1 tail)

    let rec listCount input =
        match input with
        | [] -> 0
        | _ :: tail -> 1 + (listCount tail)

    let rec count collection =
        match collection with
        | ArrayType items -> arrayCount items
        | SeqType items -> seqCount items
        | ListType items -> listCount items

    let x = CollectionItem.ArrayType [| 1; 2; 3; 4 |]
    let y = CollectionItem.ListType [ "One"; "Two"; "Three" ]

    let z =
        CollectionItem.SeqType(
            seq {
                1.0
                2.0
                3.0
                4.0
            }
        )

    let xCount = count x
    let yCount = count y
    let zCount = count z

    // One last example is a linked list.
    // It's a "recursive" data structure because it can be defined in terms of itself.
    // Also known as an "algebraic" data type.
    type LinkedList<'a> =
        | Value of 'a
        | Node of 'a * LinkedList<'a> // Notice this line refers to the data structure itself.

    let rec countLinkedList (input: LinkedList<'a>) =
        match input with
        | Value _ -> 1
        | Node(_, list) -> 1 + countLinkedList list

    // A linked list of only one node.
    let linked = Value 1

    // A longer linked list.
    let linkedBig = Node(1, Node(2, Node(3, Node(4, Value(5)))))

    // To prepend a new head.
    let appendLinked = Node(0, linkedBig)

    let smallListCount = countLinkedList linked
    let linkedListCount = countLinkedList linkedBig

// Try to experiment on your own to create a tree.

module Currying =
    // In F#, every function only takes one parameter.
    // When you see a function that takes more than one parameter it is being broken down internally
    // into more functions with each function taking it's own parameter.

    /// This is a function that takes one parameter but there is another function
    /// we don't see that takes another parameter inside.
    let add x y = x + y

    let addCur x =
        let subAdd y = x + y
        subAdd

    // These two functions are equivalent.
    let y = add 1 2
    let x = addCur 2 3

// Because of this, we can compose functions like we might compose data.

module PartialApplication =
    // Partial application is a continuation on the idea that we can compose functions.
    // Because every function takes only one argument we can compose them up.

    // Supposed we want to be able to read the config file we made earlier both if it's xml and if it's json.
    type Config =
        { GetBmi: bool
          GetTotalCholesterol: bool
          GetRiskScore: bool
          RiskThreshold: float }

    let readConfig configReader config =
        printfn "The reason you would do this is so you can have data that is shared."
        // This is like using interfaces or abstract classes in OOP languages.
        configReader config

    let jsonReader (config: string) =
        let configDoc = JsonDocument.Parse(config)
        let root = configDoc.RootElement
        let getBmi = root.GetProperty("getBmi").GetBoolean()
        let getTotalCholesterol = root.GetProperty("getTotalCholesterol").GetBoolean()
        let getRiskScore = root.GetProperty("getRiskScore").GetBoolean()
        let riskThreshold = root.GetProperty("riskThreshold").GetDouble()

        { GetBmi = getBmi
          GetTotalCholesterol = getTotalCholesterol
          GetRiskScore = getRiskScore
          RiskThreshold = riskThreshold }

    let jsonConfigReader = readConfig jsonReader

    let xmlReader (config: string) =
        let configDoc = XmlDocument()
        configDoc.LoadXml(config)
        let root = configDoc.DocumentElement
        let getBmi = bool.Parse(root.SelectSingleNode("getBmi").Value)

        let getTotalCholesterol =
            bool.Parse(root.SelectSingleNode("getTotalCholesterol").Value)

        let getRiskScore = bool.Parse(root.SelectSingleNode("getRiskScore").Value)
        let riskThreshold = Double.Parse(root.SelectSingleNode("riskThreshold").Value)

        { GetBmi = getBmi
          GetTotalCholesterol = getTotalCholesterol
          GetRiskScore = getRiskScore
          RiskThreshold = riskThreshold }

    let xmlConfigReader = readConfig xmlReader
