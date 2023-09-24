// FileUtility.cs
namespace ExtractFeatures;

public class FileUtility
{
    /// <summary>
    /// Method that reads the measurement results and test results from the csv files,
    /// returns a tuple of string arrays containing the measurement results and test results.
    /// </summary>
    public static (string[], string[]) ReadFile(string measuremetPath, string testPath)
    {
        try
        {
            if (!File.Exists(measuremetPath))
                throw new FileNotFoundException("File not found", measuremetPath);
            
            if (testPath == null)
            {
                return (File.ReadAllLines(measuremetPath), null);
            }
            else
            {
                if (!File.Exists(testPath))
                    throw new FileNotFoundException("File not found", testPath);
                return (File.ReadAllLines(measuremetPath), File.ReadAllLines(testPath));
            }  
                
        }
        catch (Exception e)
        {
            throw new Exception(e.ToString());
        }
        return (null, null);
    }

    /// <summary>
    /// Method that writes the extracted features to a csv file.
    /// </summary>
    public static bool WriteFile(List<ExtractedFeatures> afterExtracts, string path)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(Helper.ExtractedFeaturesNames);
                afterExtracts.ForEach(x => sw.WriteLine(x.ToString()));
                return true;
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.ToString());
        }
        return false;
    }
}