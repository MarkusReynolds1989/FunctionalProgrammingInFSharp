// A namespace just says that all this code should be available under this name.
namespace TopicExampleCode

// Modules allow us to separate the code up to bite sized chunks.
// Programs are read from top to bottom.
// We are allowed to have submodules.
module Basics =
    // When we assign a variable with let it is immutable.
    let x: int = 4

    // This value cannot be changed.
    // x = 4 the equals sign is a comparator, not an assignment.

    // If you want to have a variable to mutate, you must use the mutable keyword.
    // Notice we don't need the type name, usually the compiler can figure out the type for us.
    let mutable y = 5
    // Then you can use the left arrow to assign.
    y <- 6

    // For the most part, we don't need to use mutable at all.

    // Problem: I want to mutate a Person, they are 25 but they should actually be 26!
    type Person = { age: int; name: string }

    // We can also optionally type with : and then the type.
    let tom: Person = { age = 25; name = "tom" }
    // F# is "strongly typed" that means each variable has one type and cannot be changed to be another type.

    // Solution: Just update the value and reassign it to tom, tom is "shadowing" the earlier value
    // The "with" keyword allows us to update records. Records are what we will use to bundle up our types similar to
    // what a class might do.
    // But in contrast, a record is just data.
    let tomf = { tom with age = 26 }


module ExampleCollections =
    // More interestingly, let's move onto collections.
    // In many languages we use collections like arrays or resizeable arrays and hashmaps.
    // In F# there are 4 main types of collections.

    // This signifies and array of integers.
    // An array has a set length and the elements of the array are mutable.
    let ages = [| 1; 2; 3; 4 |]

    // Notice that we are allowed to do this without any issues.
    ages[0] <- 2

    // Lists are sort of the "default" data collection. We should use them a lot, especially when we are
    // prototyping code. Lists are great for small collections of data but get slower
    // to access the more data you add. If you are doing a linear operation on a collection
    // it can still make sense to use a list.
    let names = [ "tom"; "frank"; "tim" ]

    // The elements are not mutable.
    // names[0] <- "tony" this will throw an error

    // Next up we have sequences, these are a great way to work on large amounts of data.
    // It is a "lazy" collection, this means that it will do every operation only when it must.
    // Whereas if we do an operation on a list it will do it as soon as it can, an operation on a sequence
    // only happens when it absolutely has to.
    let bmiData =
        seq {
            25.1
            22.3
            19.9
            30.8
            15.7
        }

    // Finally, we have maps. Maps are hashmaps or dictionaries in other languages.
    let bmiWithName =
        seq {
            ("Tom", 25.1) // This is what's call a "Tuple", it is just two pieces of data separated by a comma.
            ("Tim", 22.9) // The left item becomes the key and the right item becomes the value.
            ("Frank", 30.0) // So this starts out as a sequence of key value pairs as tuples.
        }
        |> Map.ofSeq // Then we pipe the sequence into a build in function to make it into a map.

module BuiltInFunctions =
    open System
    // The key to understanding functional programming is to remember to avoid doing work yourself!
    // There is a chance there is a built in function for you to use over a collection and it's usually
    // better to use it.

    // Built in functions live in the module as the collection they work on.
    let normalizeBmi =
        Seq.map (fun (bmi: float) -> Math.Floor bmi) ExampleCollections.bmiData

// In this case, Seq.map is a function called map under the Seq module.
// You can see we are also doing this by calling ExampleCollections with the data we added there.
// Seq.map takes a function and some data and returns a sequence with that function applied
// to every element of the sequence.
