// Comparators.cs
namespace ExtractFeatures;
/// <summary>
/// A comparator record represents the comparators used to avaluate a test.
/// </summary>
public record Comparator()
{
    public int GELE { get; set; }
    public int EQ { get; set; }
    public int LOG { get; set; }
    public int GTLT { get; set; }

    public override string ToString() => $"{GELE},{EQ},{LOG},{GTLT}";
}
