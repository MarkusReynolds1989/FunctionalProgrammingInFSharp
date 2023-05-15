namespace MedicalCheckupF

module PersonInformation =
    type Person =
        { Id: int
          Name: string
          Height: float
          Weight: float
          Ldl: int
          Hdl: int }

    let getBmi (person: Person) : float = person.Weight / person.Height ** 2
    let getTotalCholesterol (person: Person) : int = person.Hdl + person.Ldl
    let setWeight (person: Person) (weight: double) : Person = { person with Weight = weight }