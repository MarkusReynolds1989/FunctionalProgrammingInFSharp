namespace MedicalCheckupImperative;

// Module level declaration.
public static class PersonInformation
{
    // Plain Data Object - Struct.
    public class Person
    {
        public int Id;
        public string Name;
        public double Height;
        public double Weight;
        public int Ldl;
        public int Hdl;
    }

    // Initialization function.
    public static Person CreatePerson(int id,
                                      string name,
                                      double height,
                                      double weight,
                                      int ldl,
                                      int hdl)
    {
        var person = new Person();
        person.Id = id;
        person.Name = name;
        person.Height = height;
        person.Weight = weight;
        person.Ldl = ldl;
        person.Hdl = hdl;
        return person;
    }

    // Mutation is free to use anywhere, no encapsulation is implied.
    public static void SetWeight(Person person, double weight) => person.Weight = weight;
    public static double GetBmi(Person person) => person.Weight / Math.Pow(person.Height, 2);
    public static double GetTotalCholesterol(Person person) => person.Hdl + person.Ldl;
}