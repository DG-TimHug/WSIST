using Microsoft.EntityFrameworkCore;
using WSIST.Engine;
using WSIST.Engine.Persistence;
using WSIST.Web.Data;

namespace WSIST.Web.Data.Repositories;

public class DbTestRepository : ITestRepository
{
    private readonly WsistDbContext _db;

    public DbTestRepository(WsistDbContext db)
    {
        _db = db;
    }

    public async Task<List<Test>> GetAllAsync()
    {
        return await _db.Tests.ToListAsync();
    }

    public async Task AddAsync(Test test)
    {
        _db.Tests.Add(test);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Test test)
    {
        _db.Tests.Update(test);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var test = await _db.Tests.FindAsync(id);
        if (test != null)
        {
            _db.Tests.Remove(test);
            await _db.SaveChangesAsync();
        }
    }
}