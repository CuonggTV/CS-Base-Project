using Microsoft.EntityFrameworkCore;

namespace CS_Base_Project.DAL.Data.Entities;

public class AppDbContext : DbContext
{
    public DbSet<AccountEntity> Accounts { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}