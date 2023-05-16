module Tests

open AdvancedFunctionalProgramming.RecursiveDataTypes
open Xunit
open MedicalCheckupF.PersonInformation
open MedicalCheckupImperative
open MedicalCheckup
open FunctionalProgramming

[<Fact>]
let ``Person BMI Test`` () =
    let person =
        { Id = 0
          Name = "Tom"
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
