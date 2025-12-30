namespace WSIST.Engine;

public class Test
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public required Subjects Subject { get; set; }
    public required DateOnly DueDate { get; set; }
    public required TestVolume Volume { get; set; }
    public required PersonalUnderstanding Understanding { get; set; }
    public double Grade { get; set; }

    public enum Subjects
    {
        Math,
        English,
        French,
        German,
        Chemistry,
        Other,
    }

    public enum TestVolume
    {
        VeryLow,
        Low,
        Medium,
        Average,
        High,
        VeryHigh,
    }

    public enum PersonalUnderstanding
    {
        VeryLow,
        Low,
        Medium,
        Average,
        High,
        VeryHigh,
    }
}
