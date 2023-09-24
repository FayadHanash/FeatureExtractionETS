// TestResultFeatures.cs
namespace ExtractFeatures;
/// <summary>
/// A test result features class represents the features of the test results.
/// </summary>
public class TestResultFeatures
{
    public int TestResultID { get; set; }
    public string Timestamp { get; set; }
    public float ExecutionTime { get; set; }
    public int Status { get; set; }
}
