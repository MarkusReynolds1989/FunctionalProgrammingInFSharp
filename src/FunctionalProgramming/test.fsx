open System
open System.IO

let x = 3

x + 4

try
    File.ReadAllLines("")
with :? Exception as ex ->
    failwithf $"Error: {ex.Message}"
