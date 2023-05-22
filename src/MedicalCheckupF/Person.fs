namespace MedicalCheckupF

module PersonInformation =
    type Person =
        { Id: int
          Name: string
          Age: int
          Height: float
          Weight: float
          Ldl: int
          Hdl: int }
    
    let getBmi (person: Person) : float = person.Weight / person.Height ** 2
    let getTotalCholesterol (person: Person) : float = float (person.Hdl + person.Ldl)
    let setWeight (person: Person) (weight: double) : Person = { person with Weight = weight }
    let getRiskScore (person: Person) : float =
        getBmi person + getTotalCholesterol person