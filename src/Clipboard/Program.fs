open System
open System.IO

[<EntryPoint>]
let main args =
    if (not <| File.Exists("data.txt")) then
        File.Create("data.txt").Close()
    
    0
