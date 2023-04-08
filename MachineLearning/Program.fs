namespace MachineLearning

open System
open System.IO
open Microsoft.ML
open Microsoft.ML.Data

[<CLIMutable>]
type Data =
    {
        mutable Label: string
    }
    
module Main =
    [<EntryPoint>]
    let main args =
        
        0
