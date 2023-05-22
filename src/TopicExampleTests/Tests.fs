module Tests

open AdvancedFunctionalProgramming.RecursiveDataTypes
open FunctionalProgramming
open Xunit
open MedicalCheckupF.PersonInformation
open MedicalCheckupImperative
open MedicalCheckup

[<Fact>]
let ``Person BMI Test`` () =
    let person =
        { Id = 0
          Name = "Tom"
          Age = 23
          Height = 1.83
          Weight = 80.0
          Ldl = 10
          Hdl = 10 }

    Assert.InRange(getBmi person, 23.0, 23.9)

[<Fact>]
let ``OOP BMI Test`` () =
    let person = Person(0, "tom", 1.83, 80.0, 10, 10)

    Assert.InRange(person.GetBmi(), 23.0, 23.9)

[<Fact>]
let ``Imperative BMI Test`` () =
    let person = PersonInformation.CreatePerson(0, "Tom", 1.83, 80.0, 10, 10)

    Assert.InRange(PersonInformation.GetBmi person, 23.0, 23.9)

[<Fact>]
let ``Area Circle Test`` () =
    let circle = Shape.Circle 5.0
    let area = round (getArea circle)
    Assert.Equal(79.0, area)

[<Fact>]
let ``Area Square Test`` () =
    let square = Shape.Square 2.0
    let area = getArea square
    Assert.Equal(4.0, area)

[<Fact>]
let ``Area Rectangle Test`` () =
    let rectangle = Shape.Rectangle(2.0, 3.0)
    let area = getArea rectangle
    Assert.Equal(6.0, area)

[<Fact>]
let ``Seq Count Test`` () =
    let sequence =
        CollectionItem.SeqType(
            seq {
                1.0
                2.0
                3.0
                4.0
            }
        )

    Assert.Equal(4, count sequence)

[<Fact>]
let ``People import test`` () =
    Assert.NotEmpty(HealthData.patients HealthData.healthDataLines)

[<Fact>]
let ``All People Functions Return the Same`` () =
    Assert.Equivalent(
        HealthData.patients HealthData.healthDataLines,
        HealthData.patientsRecursive (HealthData.healthDataLines |> Seq.toList)
        |> List.toSeq
    )

    Assert.Equivalent(
        HealthData.patients HealthData.healthDataLines,
        HealthData.patientsApplicative HealthData.healthDataLines
    )

[<Fact>]
let ``Apply Business Rules Works`` () =
    let data = HealthData.patients HealthData.healthDataLines
    let rules = HealthData.businessRules HealthData.configBusinessRules
    let results = HealthData.applyAllBusinessRules rules data
    Assert.NotEmpty results

[<Fact>]
let ``Filter High Risk `` () =
    let data = HealthData.patients HealthData.healthDataLines
    let rules = HealthData.businessRules HealthData.configBusinessRules
    let results = HealthData.applyAllBusinessRules rules data
    let highRisk = HealthData.filterHighRisk HealthData.configBusinessRules results
    Assert.Equivalent(19, Seq.length highRisk)
