using System.Text.Json;
using Microsoft.Data.SqlClient;

namespace WSIST.Engine;

public class TestManagement
{
    private const string Filename =
        @"C:\Development\Git Projects\WSIST\WSIST\WSIST.Engine\tests.json";
    public List<Test> Tests = new();

    public TestManagement(Database database)
    {
        database.Query("SELECT * FROM Test WHERE Id = @Id", new Dictionary<string, object>
        {
            { "Id", 123 }
        });
        
        TestLoader();
    }

    private static Guid IdMaker()
    {
        var id = Guid.NewGuid();
        Console.Write(id);
        return id;
    }

    public void NewTestMaker(
        string title,
        Test.Subjects subject,
        DateOnly dueDate,
        Test.TestVolume volume,
        Test.PersonalUnderstanding understanding,
        double? grade
    )
    {
        Test newTest = new()
        {
            Id = IdMaker(),
            Title = title,
            Subject = subject,
            DueDate = dueDate,
            Volume = volume,
            Understanding = understanding,
            Grade = grade,
        };
        TestAssistants.GradeVerifier(dueDate, grade);
        Tests.Add(newTest);
        SaveTests(Tests);
    }

    public void TestEditor(
        Guid id,
        string title,
        Test.Subjects subject,
        DateOnly dueDate,
        Test.TestVolume volume,
        Test.PersonalUnderstanding understanding,
        double? grade
    )
    {
        foreach (var test in Tests)
        {
            if (test.Id == id)
            {
                test.Subject = subject;
                test.Title = title;
                test.DueDate = dueDate;
                test.Volume = volume;
                test.Understanding = understanding;
                test.Grade = TestAssistants.GradeVerifier(dueDate, grade);
                SaveTests(Tests);
            }
        }
    }

    public void TestRemover(Guid id)
    {
        var test = Tests.FirstOrDefault(test => test.Id == id);
        if (test == null)
            return;
        Tests.Remove(test);
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
            Tests = JsonSerializer.Deserialize<List<Test>>(jsonString) ?? [];
            if (string.IsNullOrWhiteSpace(jsonString)) { }
        }
    }

    public void Refresh()
    {
        TestLoader();
        Console.WriteLine("Refreshed");
    }
}
