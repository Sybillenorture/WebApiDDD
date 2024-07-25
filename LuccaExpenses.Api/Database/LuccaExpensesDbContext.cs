using LuccaExpenses.Api.Configuration;
using LuccaExpenses.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

public class LuccaExpensesDbContext : DbContext
{
    public LuccaExpensesDbContext(DbContextOptions<LuccaExpensesDbContext> options)
        : base(options)
    {
    }

    public DbSet<Expense> Expense { get; set; } = default!;
    public DbSet<User> User { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ExpenseConfiguration());

        // Seed data for Users
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, FirstName = "Anthony", LastName = "Stark", Currency = "USD" },
            new User { Id = 2, FirstName = "Natasha", LastName = "Romanova", Currency = "RUB" }
        );

        modelBuilder.Entity<Expense>()
       .HasIndex(e => new { e.UserId, e.Date, e.Amount })
       .IsUnique();
    }
}
