// Extract.cs
// Author: Fayad Al Hanash
namespace ExtractFeatures;
/// <summary>
/// A class to extract features from the measurement results and test results.
/// </summary>
public class Extract
{
    /// <summary>
    /// Method that extracts the features 
    /// Calls the ReadFile method to get the measurements and test result arrays.
    /// Calls GetMeasurementResultFeatures and GetTestResultFeatures methods to create list of 
    /// MeasurementResultFeatures and TestResultFeatures objects
    /// Calls the GetMergedList method to merge the lists into one list of MeasurementResultFeatures objects
    /// Groups the measurement result features based on the test result ID
    /// Creates a new list of ExtractedFeatures object
    /// Iterates through each group to calculate the measurements and frequencies 
    /// Calls the WriteFile method to save the features in the file
    /// </summary>
    public bool StartExtracting(string measuremetPath, string testPath, string desPath)
    {
        (string[] measurementArray, string[] testArray) = FileUtility.ReadFile(measuremetPath, testPath);
        if (measurementArray == null) return false;
        List<MeasurementResultFeatures> measurementResultList = GetMeasurementResultFeatures(measurementArray);
        List<TestResultFeatures> testResultList = GetTestResultFeatures(testArray);
        List<MeasurementResultFeatures> mergerdList = GetMergedList(measurementResultList, testResultList);
        var groups = mergerdList.GroupBy(b => b.TestResultID);
        List<ExtractedFeatures> featureAfterList = new List<ExtractedFeatures>();
        foreach (var g in groups)
        {
            var ExtQuery = from b in g let execTime = b.ExecutionTime select execTime;
            var NumericDataQuery = from b in g let numericData = b.NumericData select numericData;
            var NumericLowLimitQuery = from b in g let numericLowLimit = b.NumericLowLimit select numericLowLimit;
            var NumericHighLimitQuery = from b in g let numericHighLimit = b.NumericHighLimit select numericHighLimit;
            var measurementIDQuery = from b in g let measurementID = b.MeasurementResultID select measurementID;
            
            var nameQuery = from n in g let name = n.Name select name;
            var comparator = from b in g let comp = b.Comparator select comp;
            var unit = from b in g let u = b.Unit select u;
            var GELE = from c in comparator where c.GELE == 1 select c.GELE;
            var EQ = from c in comparator where c.EQ == 1 select c.EQ;
            var LOG = from c in comparator where c.LOG == 1 select c.LOG;
            var GTLT = from c in comparator where c.GTLT == 1 select c.GTLT;
            var V = from u in unit where u.V == 1 select u.V;
            var µA = from u in unit where u.µA == 1 select u.µA;
            var NULL = from u in unit where u.NULL == 1 select u.NULL;

            float extMin = ExtQuery.Min();
            float numDMin = NumericDataQuery.Min();
            float numLMin = NumericLowLimitQuery.Min();
            float numHMin = NumericHighLimitQuery.Min();

            ExtractedFeatures after = new ExtractedFeatures()
            {
                TestResultID = g.Key,
                MeasurementTestFrequency = measurementIDQuery.Count(),
                NameFrequency = nameQuery.Count(),
                ExecTimeTotal = g.ElementAt(0).ExecutionTimeAll,
                ExecTimeMes = new Measurement
                {
                    Avg = ExtQuery.Average(),
                    Max = ExtQuery.Max(),
                    Min = ExtQuery.OrderBy(x => x != 0 && extMin != 0).First(), 
                    Median = Helper.MEDIAN(ExtQuery),
                    StdDev = Helper.STDDEV(ExtQuery)
                },
                NumericDataMes = new Measurement
                {
                    Avg = NumericDataQuery.Average(),
                    Max = NumericDataQuery.Max(),
                    Min = NumericDataQuery.OrderBy(x => x != 0 && numDMin != 0).First(),
                    Median = Helper.MEDIAN(NumericDataQuery),
                    StdDev = Helper.STDDEV(NumericDataQuery)
                },
                NumericLowLimitMes = new Measurement
                {
                    Avg = NumericLowLimitQuery.Average(),
                    Max = NumericLowLimitQuery.Max(),
                    Min = NumericLowLimitQuery.OrderBy(x => x != 0 && numLMin != 0).First(),
                    Median = Helper.MEDIAN(NumericLowLimitQuery),
                    StdDev = Helper.STDDEV(NumericLowLimitQuery)
                },
                NumericHighLimitMes = new Measurement
                {
                    Avg = NumericHighLimitQuery.Average(),
                    Max = NumericHighLimitQuery.Max(),
                    Min = NumericHighLimitQuery.OrderBy(x => x != 0 && numHMin != 0).First(),
                    Median = Helper.MEDIAN(NumericHighLimitQuery),
                    StdDev = Helper.STDDEV(NumericHighLimitQuery)
                },
                GELEFreq = GELE.Count(),
                EQFreq = EQ.Count(),
                LOQFreq = LOG.Count(),
                GTLTFreq = GTLT.Count(),
                VFreq = V.Count(),
                µAFreq = µA.Count(),
                NULLFreq = NULL.Count(),
                Status = g.ElementAt(0).Status
            };
            
            featureAfterList.Add(after);
        }
        return FileUtility.WriteFile(featureAfterList, desPath);
    }

    /// <summary>
    /// Method that returns a list of MeasurementResultFeatures object
    /// </summary>
    List<MeasurementResultFeatures> GetMeasurementResultFeatures(string[] arr) =>
             arr
            .Skip(1)
            .Select(line =>
            {
                string[] col = line.Split(',');
                return new MeasurementResultFeatures
                {
                    MeasurementResultID = int.Parse(col[0]),
                    TestResultID = int.Parse(col[1]),
                    Name = col[2],
                    TimeStamp = col[3].Substring(0, col[3].LastIndexOf(':')),
                    ExecutionTime = float.Parse(col[4]),
                    NumericData = float.Parse(col[5]),
                    NumericLowLimit = float.Parse(col[6]),
                    NumericHighLimit = float.Parse(col[7]),
                    Comparator = Helper.GetComparator(col[8]),
                    Unit = Helper.GetUnit(col[9]),
                    Status = Helper.GetStatus(col[10])
                };
            })
            .ToList();

    /// <summary>
    /// Method that returns a list of TestResultFeatures object
    /// </summary>
    List<TestResultFeatures> GetTestResultFeatures(string[] arr) =>
     arr
    .Skip(1)
    .Select(line =>
    {
        string[] col = line.Split(',');
        return new TestResultFeatures
        {
            TestResultID = int.Parse(col[0]),
            Timestamp = col[1].Substring(0, col[1].LastIndexOf(':')),
            ExecutionTime = float.Parse(col[2]),
            Status = Helper.GetStatus(col[3])
        };
    })
    .ToList();

    /// <summary>
    /// Method that merges list of MeasurementResultFeatures and TestResultFeatures lists and returns a new list 
    /// of MeasurementResultFeatures
    /// </summary>
    List<MeasurementResultFeatures> GetMergedList(List<MeasurementResultFeatures> mRList, List<TestResultFeatures> tRList)
    {

        var list = (from mR in mRList
                    join tR in tRList
                    on mR.TestResultID equals tR.TestResultID
                    into temp
                    from tR in temp.DefaultIfEmpty()
                    select new MeasurementResultFeatures
                    {
                        MeasurementResultID = mR.MeasurementResultID,
                        TestResultID = mR.TestResultID,
                        Name = mR.Name,
                        TimeStamp = mR.TimeStamp,
                        ExecutionTime = mR.ExecutionTime,
                        ExecutionTimeAll = tR.ExecutionTime,
                        NumericData = mR.NumericData,
                        NumericLowLimit = mR.NumericLowLimit,
                        NumericHighLimit = mR.NumericHighLimit,
                        Comparator = mR.Comparator,
                        Unit = mR.Unit,
                        Status = tR.Status
                    }).ToList();
        
        return list;
    }
}
