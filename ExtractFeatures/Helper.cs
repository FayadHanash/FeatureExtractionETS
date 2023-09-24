// Helper.cs
namespace ExtractFeatures;

public class Helper
{
    /*public static string ExtractedFeaturesNames = "TestResultID,MeasurementTestFrequency,NameFrequency," +
        "ExecTimeTotal,ExecMin,ExecMax,ExecAvg,ExecMedian,ExecStdDev,NumDataMin,NumDataMax,NumDataAvg," +
        "NumDataMedian,NumDataStdDev,NumDataLowLimMin,NumDataLowLimMax,NumDataLowLimAvg,NumDataLowLimMedian," +
        "NumDataLowLimStdDev,NumDataHighLimMin,NumDataHighLimMax,NumDataHighLimAvg,NumDataHighLimMedian," +
        "NumDataHighLimStdDev,GELEFreq,EQFreq,LOQFreq,GTLTFreq,VFreq,µAFreq,NULLFreq,Status";*/
    public static string ExtractedFeaturesNames = "ExecTimeTotal,ExecMax,ExecAvg,ExecStdDev,NumDataMin," +
        "NumDataAvg,NumDataMedian,NumDataStdDev,Status";
    public static string OrginalFeaturesNames = "measurement_result_id,test_result_id,name,timestamp," +
        "execution_time,numeric_data,numeric_low_limit,numeric_high_limit,comparator,unit,status";

    /// <summary>
    /// Method that returns a new instance of comparator and sets the value of the comparator based on the string value.
    /// </summary>
    public static Func<string, Comparator> GetComparator = x => x switch
    {
        "GELE" => new() { GELE = 1 },
        "EQ" => new() { EQ = 1 },
        "LOG" => new() { LOG = 1 },
        "GTLT" => new() { GTLT = 1 },
        _ => new(),
    };
    
    /// <summary>
    /// Method that returns a new instance of unit and sets the value of the unit based on the string value.
    /// </summary>

    public static Func<string, Unit> GetUnit = x => x switch
    {
        "V" => new Unit { V = 1 },
        "µA" => new Unit { µA = 1 },
        "NULL" => new Unit { NULL = 1 },
        _ => new Unit()
    };

    /// <summary>
    /// Method that returns the status of a test as an integer based on the string value.
    /// </summary>
    public static int GetStatus(string status)
    {
        return status switch
        {
            "passed" => 0,
            "failed" => 1,
            "error" => 2,
            "terminated" => 3,
            _ => 4,
        };
    }

    /// <summary>
    /// Method that returns the status of a test as a string based on the integer value.
    /// </summary>
    public static string GetStatus(int status)
    {
        return status switch
        {
            0 => "passed",
            1 => "failed",
            2 => "error",
            3 => "terminated",
            _ => "unknown",
        };
    }
    
    /// <summary>
    /// Method that calculates the standard deviation.
    /// </summary>
    public static float STDDEV(IEnumerable<float> s)
    {
        if (s.Count() > 0)
        {
            float x = (float) Math.Sqrt(s.Sum(d => Math.Pow(d - s.Average(), 2)) / (s.Count() - 1));
            if (double.IsNaN(x))
                return 0;
            else
                return x;
        }
        else return 0;
    }

    /// <summary>
    /// Method that calculates the median
    /// </summary>
    public static float MEDIAN(IEnumerable<float> s) =>
        s.Count() > 0 ? s.OrderBy(x => x).ElementAt((int)Math.Ceiling((double)(s.Count() - 1) / 2)) : 0;
    
}
