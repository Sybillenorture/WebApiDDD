using Lucca.ExpenseApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lucca.ExpenseApp.Infrastructure.Persistence
{
    public class ExpenseAppDbContext : DbContext
    {
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Claimant> Claimants { get; set; }

        public ExpenseAppDbContext(DbContextOptions<ExpenseAppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Claimant>().HasData(
                new Claimant { Id = 1, FirstName = "Anthony", LastName = "Stark", Currency = "USD" },
                new Claimant { Id = 2, FirstName = "Natasha", LastName = "Romanova", Currency = "RUB" }
            );
        }
    }
}