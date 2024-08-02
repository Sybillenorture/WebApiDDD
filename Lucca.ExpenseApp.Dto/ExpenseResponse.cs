namespace Lucca.ExpenseApp.Dto
{
    public class ExpenseResponse
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Currency { get; set; }
        public string ClaimantFullName { get; set; } // Format: {FirstName} {LastName}
    }
}
