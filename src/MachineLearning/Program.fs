module Main =
    open Microsoft.ML
    open Microsoft.ML.Data
    open System
    open System.IO
    open Microsoft.ML.Trainers

    [<CLIMutable>]
    type ProductEntry =
        { [<LoadColumn(0); KeyType(count = 45UL)>]
          ProductID: uint32
          [<LoadColumn(1); KeyType(count = 45UL)>]
          OtherProductID: uint32
          [<NoColumn>]
          Label: float32 }

    [<CLIMutable>]
    type Prediction = { Score: float32 }

    let assemblyFolderPath =
        Path.GetDirectoryName(Reflection.Assembly.GetExecutingAssembly().Location)

    let trainPath =
        "C:/Users/marku/home/code/f#/FunctionalProgrammingInFSharp/src/MachineLearning/transaction_list.tsv"

    let mlContext = MLContext()

    let trainData =
        let columns =
            [| TextLoader.Column("Label", DataKind.Single, 0)
               TextLoader.Column(
                   "ProductID",
                   DataKind.UInt32,
                   source = [| TextLoader.Range(0) |],
                   keyCount = KeyCount 45UL
               )
               TextLoader.Column(
                   "OtherProductID",
                   DataKind.UInt32,
                   source = [| TextLoader.Range(1) |],
                   keyCount = KeyCount 45UL
               ) |]

        mlContext.Data.LoadFromTextFile(trainPath, columns, hasHeader = true, separatorChar = '\t')

    let options =
        MatrixFactorizationTrainer.Options(
            MatrixColumnIndexColumnName = "ProductID",
            MatrixRowIndexColumnName = "OtherProductID",
            LossFunction = MatrixFactorizationTrainer.LossFunctionType.SquareLossOneClass,
            LabelColumnName = "Label",
            Alpha = 0.01,
            Lambda = 0.025
        )

    let est = mlContext.Recommendation().Trainers.MatrixFactorization(options)

    let model = est.Fit(trainData)

    let predictionEngine =
        mlContext.Model.CreatePredictionEngine<ProductEntry, Prediction>(model)

    let prediction =
        predictionEngine.Predict
            { ProductID = 1u
              OtherProductID = 15u
              Label = 0.f }

    printfn $"ProductID = 1 and OtherProductID = 15\nPredicted Score: %f{prediction.Score}"