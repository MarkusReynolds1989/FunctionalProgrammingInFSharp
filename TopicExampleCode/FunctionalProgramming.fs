module TopicExampleCode.FunctionalProgramming

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
let tom = { tom with age = 26 }
// The "with" keyword allows us to update records. Records are what we will use to bundle up our types similar to
// what a class might do.
// But in contrast, a record is just data.

// More interestingly, let's move onto collections.
// In many languages we use collections like arrays or resizeable arrays and hashmaps.
// In F# there are 4 main types of collections.

// This signifies and array of integers.
// An array has a set length and the elements of the array are mutable.
let ages = [|1;2;3;4|]

// Notice that we are allowed to do this without any issues.
ages[0] <- 2


