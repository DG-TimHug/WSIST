using Microsoft.EntityFrameworkCore;
using WSIST.Engine; // or WSIST.Engine.Models if that's where Test lives

namespace WSIST.Web.Data;

public class WsistDbContext : DbContext
{
    public WsistDbContext(DbContextOptions<WsistDbContext> options)
        : base(options)
    {
    }

    public DbSet<Test> Tests => Set<Test>();
}