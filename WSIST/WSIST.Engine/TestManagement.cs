namespace WSIST.Engine;

public abstract class TestManagement
{
    internal const string Filename = @"C:\temp\tests.json";
    

    public static Guid IdMaker()
    {
        var id = Guid.NewGuid();
        Console.Write(id);
        return id;
    }
}