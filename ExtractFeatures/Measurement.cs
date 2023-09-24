// Measurement.cs
namespace ExtractFeatures;
/// <summary>
/// A measurement class represents the calculated Min, Max, Mean, Median, 
/// and StdDev of the numerical and execution time measurments.
/// </summary>
public class Measurement
{
    public float Min { get; set; }
    public float Max { get; set; }
    public float Avg { get; set; }
    public float Median { get; set; }
    public float StdDev { get; set; }

    public override string ToString() => 
        $"{Min},{Max},{Avg},{Median},{StdDev}";
    public string ExecToString() => $"{Max},{Avg},{StdDev}";
    public string NumDataToString() => $"{Min},{Avg},{Median},{StdDev}";
    public string NumDLowToString() => $"{Avg},{Median},{StdDev}";
    public string NumDHighToString() => NumDLowToString();

}
