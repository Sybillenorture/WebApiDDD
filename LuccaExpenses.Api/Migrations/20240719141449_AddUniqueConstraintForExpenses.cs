using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuccaExpenses.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraintForExpenses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            DO $$
            BEGIN
                IF NOT EXISTS (
                    SELECT 1
                    FROM pg_indexes
                    WHERE tablename = 'Expense' AND indexname = 'IX_Expense_UserId_Date_Amount'
                ) THEN
                    CREATE INDEX IX_Expense_UserId_Date_Amount
                    ON ""Expense"" (""UserId"", ""Date"", ""Amount"");
                END IF;
            END
            $$;
        ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            DO $$
            BEGIN
                IF EXISTS (
                    SELECT 1
                    FROM pg_indexes
                    WHERE tablename = 'Expense' AND indexname = 'IX_Expense_UserId_Date_Amount'
                ) THEN
                    DROP INDEX IF EXISTS ""IX_Expense_UserId_Date_Amount"";
                END IF;
            END
            $$;
        ");
        }
    }
}
