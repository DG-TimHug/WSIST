using System.Data;
using System.Text.Json;
using Microsoft.Data.SqlClient;

namespace WSIST.Engine;

public class TestManagement
{
    private Database database;
    
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
        int id,
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

    public void TestRemover(int id)
    {
        var test = Tests.FirstOrDefault(test => test.Id == id);
        if (test == null)
            return;
        Tests.Remove(test);
        SaveTests(Tests);
    }

    private void SaveTests(List<Test> tests)
    {
        // database.Query("") // TODO: Refactor to load single test instead of all tests
    }

    private DataTable LoadAllTests()
    {
        return database.Query("SELECT title, subject, duedate, volume, understanding FROM tests;");
    }
    
    private Test LoadTest(int id)
    {
        var dataTable = database.Query(
            "SELECT title, subject, duedate, volume, understanding FROM tests WHERE id = @id;",
            new Dictionary<string, object>
            {
                { "id", id }
            }
        );

        Test Test;
        
        Test.Title = dataTable.Rows[0]["Title"].ToString();
        
        return new Test();
    }

    public void Refresh()
    {
        TestLoader();
        Console.WriteLine("Refreshed");
    }
}
