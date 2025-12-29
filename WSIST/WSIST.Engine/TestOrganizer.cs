using System.Text.Json;

namespace WSIST.Engine;

public class TestOrganizer
{
    private const string Filename = @"C:\temp\tests.json";
    public List<Test>? Tests = new List<Test>();

    private static Guid IdMaker()
    {
        var id = Guid.NewGuid();
        Console.Write(id);
        return id;
    }
    public void NewTestMaker()
    {
        Test newTest = new()
        {
            Id = IdMaker(),
            Title = "Test 2",
            Subject = "Math",
            DueDate = new DateTime(2026,01,03)
        };
        Tests.Add(newTest);
        SaveTests(Tests);
    }

    private void SaveTests(List<Test> tests)
    {
        string json = JsonSerializer.Serialize(tests, new JsonSerializerOptions{ WriteIndented = true });
        File.WriteAllText(Filename, json);
        Console.WriteLine(json);
    }

    private List<Test>? TestLoader()
    {
        if (File.Exists(Filename))
        {
            string jsonString = File.ReadAllText(Filename);
            Tests = JsonSerializer.Deserialize<List<Test>>(jsonString);
        }

        return Tests;
    }
}