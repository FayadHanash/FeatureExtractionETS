// Program.cs
// Author: Fayad Al Hanash
using ExtractFeatures;
string testPath = @"../../../test_results.csv";
string measurePath = @"../../../measurement_results.csv";
string extreactedPath = @"../../../Ext/extracted.csv";

Extract extractor = new Extract();
extractor.StartExtracting( measurePath,testPath, extreactedPath);
Console.WriteLine("Done");
