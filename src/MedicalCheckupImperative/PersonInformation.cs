namespace MedicalCheckupImperative;

public static class PersonInformation
{
    public class Person
    {
        public int Id;
        public string Name;
        public double Height;
        public double Weight;
        public int Ldl;
        public int Hdl;
    }

    public static Person CreatePerson(int id,
                                      string name,
                                      double height,
                                      double weight,
                                      int ldl,
                                      int hdl)
    {
        var person = new Person
        {
            Id = id,
            Name = name,
            Height = height,
            Weight = weight,
            Ldl = ldl,
            Hdl = hdl
        };
        return person;
    }

    public static double GetBmi(Person person) => person.Weight / Math.Pow(person.Height, 2);
    public static double GetTotalCholesterol(Person person) => person.Hdl + person.Ldl;
}