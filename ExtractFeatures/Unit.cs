// Unit.cs
namespace ExtractFeatures;
/// <summary>
/// A unit record represents the unit of the measured values.
/// </summary>
public record Unit
{
    public int V { get; set; }
    public int µA { get; set; }
    public int NULL { get; set; }
    public override string ToString() => $"{V},{µA},{NULL}";
}
