namespace WSIST.Engine;

public class Test
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Subject { get; set; }
    public DateTime DueDate { get; set; }
    public TestOrganizer Organizer { get; set; }

    public static void Main()
    {
        TestOrganizer organizer = new();
        organizer.NewTestMaker();
    }
}
