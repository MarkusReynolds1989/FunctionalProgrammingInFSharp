namespace FunctionalProgramming

module Collections =
    // Arrays are fixed size, mutable, and indexed.
    // Notice that I don't need to specify the type of the array. That's because the compiler can infer it.
    let array = [| 1; 2; 3; 4 |]

    // Lists are immutable, singly linked lists.
    let list = [ 1; 2; 3; 4 ]

    // Sequences are lazily evaluated, immutable, and can be infinite.
    // What this means is, you can chain operations together and they won't be evaluated until you need them.
    let sequence =
        seq {
            1
            2
            3
            4
        }

module BuiltInFunctions =
    // This will only return the even numbers.
    // You may be familiar with this if you are used to using LINQ.
    let result = Seq.filter (fun x -> x % 2 = 0) [ 1; 2; 3; 4 ]

    // F# is all about chaining these function calls together.
    // The benefit is anyone who is familiar with the language can read this and understand what it's doing.
    // Whereas in a language where you need to use a loop, you need to read the entire loop to understand what it's doing.

    // This will "map" over the given array and add one to each number.
    // Map is a function that takes a function and a collection, and returns a new collection with the function applied to each item.
    let addedItems = Array.map (fun x -> x + 1) [| 1; 2; 3; 4 |]

    // A common pattern you might see in functional programming is "filter map reduce" this is an easy way to
    // Get only the data you need, do functions on them, and finally do some sort of function to reduce the values down to a single value.
    // This works but it is very deeply nested, I will show you how to avoid this in the next section.
    let finalResult =
        (Seq.reduce (+) (Seq.map (fun (x: int) -> x + 1) (Seq.filter (fun (x: int) -> x > 1) [ 1; 2; 3; 4 ])))



module Piping =
    // This avoids the deeply nested function calls.
    // Now it's a chain, we chain one function to the next.
    // This is known as applicative programming or point free programming.
    let result =
        [ 1; 2; 3; 4 ]
        |> Seq.filter (fun x -> x > 1)
        |> Seq.map (fun x -> x + 1)
        |> Seq.reduce (+)

    // You can chain anywhere where you would put data into a function, but the data will always be the last arg
    // into that function.
    let lower = "One" |> (fun x -> x.ToLower())
// We pipe the string "One" into the closure of (fun x ->. The x represents the data we are sending into the
// closure.

module Closures =
    // When we pipe the string "one" into this anonymous function, it closes around it.
    let lower = "One" |> (fun x -> x.ToLower())

    // We can close over some mutable state, in this case a string called name.
    let person initial =
        let name = ref initial

        // Instead of private, we just don't export the function.
        let lowerCaseName = name.Value |> (fun (x: string) -> x.ToLower())

        let set newValue = name.Value <- newValue
        let getName () = name.Value

        // Then we can return the functions as a tuple.
        (set, getName)

    // This gives us the concept of encapsulation from OOP, now we can enforce how someone can interact with our data.
    // Encapsulation helps us to hide implementation details and the ability to modify data in a way we don't intend from
    // the outside world.
    let setTomsName, getTomsName = person "Tom"
    printfn $"{getTomsName ()}"
    setTomsName "tom"
    printfn $"{getTomsName ()}"

module ReturningFunctions =
    // We saw above how closures can enable us to encapsulate data, and in that case we passed functions back
    // that operated on some internal bits of mutable data.
    // We can use functions anywhere we could use a variable or an object, they are "first class" in F#.
    // This is also true of C#, where there is a concept of "delegates" that enable you to pass functions around
    // as data.
    let adder x y = x + y

    // In this case, we can create a function that takes another function.
    // The "fn" argument is a slot for a function we can call on numOne and numTwo below.
    let calculator fn numOne numTwo = fn numOne numTwo

    // This will add 1 and 2 together.
    let result = calculator adder 1 2

    // This isn't the most interesting example, but it's the basis for some more interesting things we can do
    // such as currying.
    let multiplier x y = x * y
    let divider x y = x / y
    let subtracter x y = x - y

    // Because functions are data we can add them to a list or a sequence or assign them to other variables.
    let mathFunctions = [ adder; multiplier; divider; subtracter ]
    // Then if we had some numbers we wanted to work on in sequence we could call those functions against
    // a number list.
    let resolveEquations numOne numTwo mathFunctions =
        mathFunctions |> List.map (fun mathFunction -> mathFunction numOne numTwo)

    // As we can see, we can plug in functions and lists of functions where ever we want.
    // Functions can return other functions as well.
    let results =
        [ (1, 2); (3, 4); (5, 6); (7, 8) ]
        |> List.map (fun (numbers: int * int) ->
            let num1, num2 = numbers
            resolveEquations num1 num2 mathFunctions)
