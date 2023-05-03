namespace MedicalCheckupF

module PersonInformation =
    type Person =
        { Id: int
          Name: string
          Height: float
          Weight: float
          Ldl: int
          Hdl: int }

    let createPerson id name height weight ldl hdl =
        { Id = id
          Name = name
          Height = height
          Weight = weight
          Ldl = ldl
          Hdl = hdl }

    let getBmi (person: Person) : float = person.Weight / person.Height ** 2

    let getTotalCholesterol (person: Person) : int = person.Hdl + person.Ldl
