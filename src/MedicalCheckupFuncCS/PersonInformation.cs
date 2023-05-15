namespace MedicalCheckupFuncCS;

// Module.
public static class PersonInformation
{
    public record Person(int Id,
                         string Name,
                         double Height,
                         double Weight,
                         int Ldl,
                         int Hdl);

    public static double GetBmi(Person person) => person.Weight / Math.Pow(person.Height, 2);
    public static double GetTotalCholesterol(Person person) => person.Hdl + person.Ldl;
    public static Person SetWeight(Person person, double weight) => person with {Weight = weight};
}