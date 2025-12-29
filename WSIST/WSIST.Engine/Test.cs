namespace WSIST.Engine;

public class Test
{
    public Guid Id = Guid.NewGuid();
    public required string Title { get; set; }
    public required string Subject { get; set; }
    public DateTime DueDate { get; set; }
}