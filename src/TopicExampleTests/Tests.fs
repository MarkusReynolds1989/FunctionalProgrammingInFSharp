module Tests

open Xunit
open MedicalCheckupF.PersonInformation
open MedicalCheckup
open MedicalCheckupImperative

[<Fact>]
let ``Person BMI Test`` () =
    let person = createPerson 0 "Tom" 1.83 80.0 10 10

    Assert.InRange(getBmi person, 23.0, 23.9)

[<Fact>]
let ``OOP BMI Test`` () =
    let person = Person(0, "tom", 1.83, 80.0, 10, 10)

    Assert.InRange(person.GetBmi(), 23.0, 23.9)

[<Fact>]
let ``Imperative BMI Test`` () =
    let person = PersonInformation.CreatePerson(0,"Tom",1.83,80.0,10,10)
    
    Assert.InRange(PersonInformation.GetBmi person, 23.0, 23.9)