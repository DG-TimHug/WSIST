using System.Data;
using System.Text.Json;
using Microsoft.Data.SqlClient;

namespace WSIST.Engine;

public class TestManagement
{
    private Database database;
    

    public TestManagement(Database database)
    {
        database.Query("SELECT * FROM Test WHERE Id = @Id", new Dictionary<string, object>
        {
            { "Id", 123 }
        });
    }
    
    //TODO: Global
    // - Remove all mentions of the List test and load tests individually
    // - Rewrite Save and Load methods
    // - Figure out how Tests now need to be saved...
    
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
        SaveTests(subject, title, dueDate, volume, understanding, grade);
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
        foreach (var test in Tests) // TODO: Figure Out how tests need to be saved because this shi wont work anymore
        {
            if (test.Id == id)
            {
                test.Subject = subject;
                test.Title = title;
                test.DueDate = dueDate;
                test.Volume = volume;
                test.Understanding = understanding;
                test.Grade = TestAssistants.GradeVerifier(dueDate, grade);
                SaveTests(subject, title, dueDate, volume, understanding, grade);
            }
        }
    }

    public void TestRemover(int id)
    {
        var dataTable = database.Query(
            "DELETE *  FROM tests WHERE id = @id;",
            new Dictionary<string, object>
            {
                { "id", id }
            }
        );
    }

    private void SaveTests(Test.Subjects subjects,
        string title,
        DateOnly dueDate,
        Test.TestVolume volume,
        Test.PersonalUnderstanding understanding,
        double? grade)
    {
        // database.Query("") // TODO: Create insert Statment that saves a singele Test into the Database...
    }

    private DataTable LoadAllTests()
    {
        return database.Query("SELECT title, subject, duedate, volume, understanding FROM tests;");
    }
    
    private Test LoadTest(int id) // Holy chaos figure out how to load specific Test based on Pokedex
    {
        var dataTable = database.Query(
            "SELECT title, subject, duedate, volume, understanding FROM tests WHERE id = @id;",
            new Dictionary<string, object>
            {
                { "id", id }
            }
        );

        Test test;
        
        test.Title = dataTable.Rows[0]["Title"].ToString();
        
        return new Test();
    }

    public void Refresh()
    {
        LoadAllTests();
        Console.WriteLine("Refreshed");
    }
}
