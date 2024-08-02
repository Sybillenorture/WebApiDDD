namespace Lucca.ExpenseApp.Domain.Entities
{
    public class Claimant
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Currency { get; set; }

        public ICollection<Expense> Expenses { get; set; } // Navigation property
    }
}
