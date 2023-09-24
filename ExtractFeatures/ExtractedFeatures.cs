// ExtractedFeatures.cs
namespace ExtractFeatures;
/// <summary>
/// A feature class represents the extracted features (features after extraction).
/// </summary>
public class ExtractedFeatures 
{
    public int TestResultID { get; set; }
    public int MeasurementTestFrequency { get; set; }
    public int NameFrequency { get; set; }
    public float ExecTimeTotal { get; set; }
    public  Measurement ExecTimeMes { get; set; }
    public  Measurement NumericDataMes { get; set; }
    public  Measurement NumericLowLimitMes { get; set; }
    public  Measurement NumericHighLimitMes { get; set; }
    public int GELEFreq { get; set; }
    public int EQFreq { get; set; }
    public int LOQFreq { get; set; }
    public int GTLTFreq { get; set; }
    public int VFreq { get; set; }
    public int µAFreq { get; set; }
    public int NULLFreq { get; set; }
    public int Status { get; set; }

    /*public override string ToString() => $"{TestResultID},{MeasurementTestFrequency}," +
        $"{NameFrequency},{ExecTimeTotal},{ExecTimeMes},{NumericDataMes},{NumericLowLimitMes}," +
        $"{NumericHighLimitMes},{GELEFreq},{EQFreq},{LOQFreq},{GTLTFreq},{VFreq},{µAFreq},{NULLFreq},{(int)Status}";*/
    public override string ToString() => $"{ExecTimeTotal},{ExecTimeMes.ExecToString()}," +
        $"{NumericDataMes.NumDataToString()},{(int)Status}";

}
