namespace FunctionalProgramming

open System.Text.Json
open MedicalCheckupF
open MedicalCheckupF.PersonInformation

// Import and clean some data.

// First off we need to import the data, this is a csv so let's import it as lines and then we can break it
// apart on commas.

module HealthData =
    open System
    open System.IO

    let healthData =
        File.ReadAllLines "C:/Users/marku/home/code/f#/FunctionalProgrammingInFSharp/src/data/person_health_data.csv"

    type Config =
        { GetBmi: bool
          GetTotalCholesterol: bool
          GetRiskScore: bool
          RiskThreshold: float }

    let config =
        File.ReadAllText
            "C:/Users/marku/home/code/f#/FunctionalProgrammingInFSharp/src/FunctionalProgramming/config.json"

    let configBusinessRules =
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

    let healthDataLines =
        // Skip the first line because that's the column names.
        healthData |> Seq.skip 1 |> Seq.map (fun line -> line.Split(","))

    let logPerson (line: string array) : Person =
        { Id = int line[0]
          Name = line[1]
          Age = int line[2]
          Height = float line[3]
          Weight = float line[4]
          Ldl = int line[5]
          Hdl = int line[6] }

    // Three ways to do this now, we can do it the imperative looking way by using a foreach in a seq.
    // There is nothing wrong with doing it this way, we would call it the "pragmatic" way to
    // accomplish the task.
    let patients people : seq<Person> =
        seq {
            for line in people do
                logPerson line
        }

    // Recursive approach.
    let rec patientsRecursive (people: list<string array>) : list<Person> =
        people
        |> function
            | [] -> []
            | head :: tail -> List.append [ (logPerson head) ] (patientsRecursive tail)

    // We can also use built in functions.
    let patientsApplicative people = people |> Seq.map logPerson

    // Now let's imagine that the functionality we want to run on the people is not included in the code itself,
    // but an outside config file, dsl, or from a database.
    // This allows us to be more flexible with our business rules because they are added at runtime by someone
    // who isn't a programmer.

    /// This function selectively adds functions to apply based upon the config input.
    let businessRules config =
        seq {
            if config.GetBmi then
                yield getBmi

            if config.GetTotalCholesterol then
                yield getTotalCholesterol

            if config.GetRiskScore then
                yield getRiskScore
        }

    type PersonRiskProfile =
        { Person: Person
          Bmi: float
          TotalCholesterol: float
          RiskScore: float }

    let applyBusinessRules rules data =
        seq {
            for rule in rules do
                yield rule data
        }

    let createPersonRiskProfile rules person =
        let values = applyBusinessRules rules person

        seq {
            { Person = person
              Bmi = Seq.item 0 values
              TotalCholesterol = Seq.item 1 values
              RiskScore = Seq.item 2 values }
        }

    let applyAllBusinessRules rules data =
        data
        |> Seq.fold (fun acc value -> Seq.append (createPersonRiskProfile rules value) acc) Seq.empty

    let filterHighRisk config data =
        data |> Seq.filter (fun datum -> datum.RiskScore > config.RiskThreshold)
