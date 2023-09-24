// MeasurementResultFeatures.cs
namespace ExtractFeatures;
/// <summary>
/// A measurement result features class represents the features of the measurement results.
/// </summary>
public class MeasurementResultFeatures
{
    public int MeasurementResultID { get; set; }
    public int TestResultID { get; set; }
    public string Name { get; set; }
    public string TimeStamp { get; set; }
    public float ExecutionTime { get; set; }
    public float ExecutionTimeAll { get; set; }
    public float NumericData { get; set; }
    public float NumericLowLimit { get; set; }
    public float NumericHighLimit { get; set; }
    public Comparator Comparator { get; set; }
    public Unit Unit { get; set; }
    public int Status { get; set; }
}
