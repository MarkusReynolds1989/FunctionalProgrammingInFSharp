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

module Closures =
    // Show how we call anonymous functions on data.
    let data = "Test"
    "Test" |> 
module ReturningFunctions =
    // I have a DU of items and I need to determine which function to call, so I build a helper
    // function that returns a function and maybe even data depending on
    // which branch gets hit.
    ()
