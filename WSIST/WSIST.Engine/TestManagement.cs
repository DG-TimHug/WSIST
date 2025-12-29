using System.Text.Json;

namespace WSIST.Engine;

public class TestManagement
{
    private const string Filename = @"C:\temp\tests.json";
    public List<Test> Tests = new List<Test>();

    public TestManagement()
    {
        TestLoader();
    }

    private static Guid IdMaker()
    {
        var id = Guid.NewGuid();
        Console.Write(id);
        return id;
    }

    public void NewTestMaker(string title, string subject, DateTime DueDate)
    {
        Test newTest = new()
        {
            Id = IdMaker(),
            Title = title,
            Subject = subject,
            DueDate = DueDate,
        };
        Tests.Add(newTest);
        SaveTests(Tests);
    }

    private void SaveTests(List<Test> tests)
    {
        string json = JsonSerializer.Serialize(
            tests,
            new JsonSerializerOptions { WriteIndented = true }
        );
        File.WriteAllText(Filename, json);
        Console.WriteLine(json);
    }

    private void TestLoader()
    {
        if (File.Exists(Filename))
        {
            string jsonString = File.ReadAllText(Filename);
            Tests = JsonSerializer.Deserialize<List<Test>>(jsonString);
            if (string.IsNullOrWhiteSpace(jsonString))
            {
                return;
            }
        }
    }

    private void TestRemover(Guid ID)
    {
        var test = Tests.FirstOrDefault(test => test.Id == ID);
        if(test == null)
            return;
        Tests.Remove(test);
        SaveTests(Tests);
    }

    public void Refresh()
    {
        TestLoader();
    }

}
