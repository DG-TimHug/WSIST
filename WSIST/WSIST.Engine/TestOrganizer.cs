using System.Text.Json;

namespace WSIST.Engine;

public class TestOrganizer
{
    public void NewTestMaker()
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

    private void SaveTests(Test test)
    {
        string jsonString = JsonSerializer.Serialize(test);
        File.AppendAllText(TestManagement.Filename, jsonString + Environment.NewLine);
        Console.WriteLine(jsonString);
    }
}