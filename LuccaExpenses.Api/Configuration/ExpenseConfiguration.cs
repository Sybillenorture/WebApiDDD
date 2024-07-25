using LuccaExpenses.Api.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace LuccaExpenses.Api.Configuration
{
    public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Date).IsRequired();
            builder.Property(e => e.Type).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Amount).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(e => e.Currency).IsRequired().HasMaxLength(3);
            builder.Property(e => e.Comment).IsRequired();

            // A user cannot declare the same expense twice (same date and amount)
            builder.HasIndex(e => new { e.UserId, e.Date, e.Amount }).IsUnique();
        }
    }
}
