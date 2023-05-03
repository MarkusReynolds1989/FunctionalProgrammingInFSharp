namespace MedicalCheckup;

public class Person
{
    private int Id { init; get; }

    private string Name { init; get; }

    // In Meters.
    private double Height { init; get; }

    // In KG.
    private double Weight { init; get; }
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
}