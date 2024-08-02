using Lucca.ExpenseApp.Domain.Enums;

namespace Lucca.ExpenseApp.Domain.Entities
{

    public class Expense
    {
        public int Id { get; set; }
        public int ClaimantId { get; set; }
        public DateTime Date { get; set; }
        public ExpenseType Type { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Comment { get; set; }

        
        public Claimant Claimant { get; set; } // Navigation property
    }
}
