namespace MachineLearning

open System
open System.IO
open Microsoft.ML
open Microsoft.ML.Trainers
open Microsoft.ML.Data

module Main =
    [<CLIMutable>]
    type Game =
        { ProductID: int
          Price: decimal
          Name: string }

    [<CLIMutable>]
    type Transaction = { TransactionID: int; ProductID: int }

    [<CLIMutable>]
    type Prediction = { Score: float32 }

    [<EntryPoint>]
    let main args =

        let path =
            @"C:\Users\marku\home\code\F#\FunctionalProgrammingInFSharp\MachineLearning\transaction_list.tsv"

        let mlContext = MLContext()

        let trainData =
            let columns =
                [| TextLoader.Column("TransactionID", DataKind.Int64, source = [| TextLoader.Range(0) |])
                   TextLoader.Column("ProductID", DataKind.Int64, source = [| TextLoader.Range(1) |]) |]

            mlContext.Data.LoadFromTextFile(path, columns, hasHeader = true, separatorChar = '\t')

        let options =
            MatrixFactorizationTrainer.Options(
                MatrixColumnIndexColumnName = "TransactionID",
                MatrixRowIndexColumnName = "ProductID",
                LossFunction = MatrixFactorizationTrainer.LossFunctionType.SquareLossOneClass,
                Alpha = 0.01,
                Lambda = 0.025
            )

        let est = mlContext.Recommendation().Trainers.MatrixFactorization(options)

        let model = est.Fit(trainData)

        let predictionEngine =
            mlContext.Model.CreatePredictionEngine<Transaction, Prediction>(model)

        let prediction = predictionEngine.Predict { TransactionID = 1; ProductID = 2 }

        printfn "%f" prediction.Score
        
        0
