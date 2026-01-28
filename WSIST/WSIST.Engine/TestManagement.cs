using WSIST.Engine.Persistence;

namespace WSIST.Engine;

public class TestManagement
{
    private readonly ITestRepository _repository;

    public List<Test> Tests { get; private set; } = new();

    public TestManagement(ITestRepository repository)
    {
        _repository = repository;
    }

    private static Guid IdMaker()
    {
        return Guid.NewGuid();
    }

    // Load all tests from DB
    public async Task LoadAsync()
    {
        Tests = await _repository.GetAllAsync();
    }

    public async Task NewTestMaker(
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

        await _repository.AddAsync(newTest);

        // keep in-memory list in sync
        Tests.Add(newTest);
    }

    public async Task TestEditor(
        Guid id,
        string title,
        Test.Subjects subject,
        DateOnly dueDate,
        Test.TestVolume volume,
        Test.PersonalUnderstanding understanding,
        double? grade
    )
    {
        var test = Tests.FirstOrDefault(t => t.Id == id);
        if (test == null)
            return;

        test.Subject = subject;
        test.Title = title;
        test.DueDate = dueDate;
        test.Volume = volume;
        test.Understanding = understanding;
        test.Grade = TestAssistants.GradeVerifier(dueDate, grade);

        await _repository.UpdateAsync(test);
    }

    public async Task TestRemover(Guid id)
    {
        var test = Tests.FirstOrDefault(t => t.Id == id);
        if (test == null)
            return;

        Tests.Remove(test);
        await _repository.DeleteAsync(id);
    }

    public async Task Refresh()
    {
        await LoadAsync();
    }
}
