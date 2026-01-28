using WSIST.Engine;
using WSIST.Engine.Persistence;

namespace WSIST.UnitTests;

public class FakeTestRepository : ITestRepository
{
    private readonly List<Test> _tests = new();

    public Task<List<Test>> GetAllAsync()
    {
        return Task.FromResult(_tests.ToList());
    }

    public Task AddAsync(Test test)
    {
        _tests.Add(test);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Test test)
    {
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        var test = _tests.FirstOrDefault(t => t.Id == id);
        if (test != null)
            _tests.Remove(test);

        return Task.CompletedTask;
    }
}