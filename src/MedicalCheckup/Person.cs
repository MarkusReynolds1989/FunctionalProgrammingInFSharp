namespace MedicalCheckup;

public class Person
{
    private int Id { init; get; }

    private string Name { init; get; }

    // In Meters.
    private double Height { get; }

    // In KG.
    private double Weight { get; set; }
    private int Ldl { init; get; }
    private int Hdl { init; get; }

    public Person(int id,
                  string name,
                  double height,
                  double weight,
                  int ldl,
                  int hdl)
    {
        Id = id;
        Name = name;
        Height = height;
        Weight = weight;
        Ldl = ldl;
        Hdl = hdl;
    }

    public double GetBmi() => Weight / Math.Pow(Height, 2);
    public double GetTotalCholesterol() => Hdl + Ldl;
    
    // Mutation is allowed, but it is encapsulated.
    public void SetWeight(double weight)
    {
        Weight = weight;
    }
}