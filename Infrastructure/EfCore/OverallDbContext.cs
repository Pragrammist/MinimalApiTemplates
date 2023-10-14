namespace EFCore;

using EFCore.Models;
using Microsoft.EntityFrameworkCore;


public class User : EntityBase<int> {
    public string Name { get; set; } = null!;
}

public class OverallDbContext : DbContext
{
    public DbSet<User> Users {get; set;} = null!;

    public OverallDbContext(DbContextOptions<OverallDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>();
        var assembly = GetType().Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        
    }
}