using Microsoft.EntityFrameworkCore;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data;

public class SmartwyreAppDbContext : DbContext
{
    public SmartwyreAppDbContext(DbContextOptions<SmartwyreAppDbContext> options) : base(options)
    {
    }

    public DbSet<Account> Accounts { get; set; }
}