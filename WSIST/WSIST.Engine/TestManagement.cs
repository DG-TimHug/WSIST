namespace WSIST.Engine;

public static class TestManagement
{
    internal static readonly string Filename = "tests.json";
    public static Guid IdMaker()
    {
        var id = new Guid();
        Console.Write(id);
        return id;
    }
}