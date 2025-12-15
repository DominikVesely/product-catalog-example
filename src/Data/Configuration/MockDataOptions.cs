namespace Data.Configuration;

internal sealed class MockDataOptions
{
    public const string SectionName = "MockData";

    public bool Enabled { get; set; } = false;
}
