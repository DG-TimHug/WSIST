using System.Text.Json;

namespace WSIST.Engine;

public class TestOrganizer
{
    public static void NewTestMaker()
    {
        Test newTest = new()
        {
            Id = TestManagement.IdMaker(),
            Title = "Test 1",
            Subject = "Math",
            DueDate = new DateTime(2026,01,03)
        };
        SaveTests(newTest);
    }

    private static void SaveTests(Test test)
    {
        string jsonString = JsonSerializer.Serialize(test);
        File.WriteAllText(TestManagement.Filename, jsonString);
        Console.WriteLine(jsonString);
    }
}