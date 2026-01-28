using WSIST.Engine;

namespace WSIST.Engine.Persistence;

public interface ITestRepository
{
    Task<List<Test>> GetAllAsync();
    Task AddAsync(Test test);
    Task UpdateAsync(Test test);
    Task DeleteAsync(Guid id);
}