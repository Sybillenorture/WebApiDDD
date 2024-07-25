namespace LuccaExpenses.Api.Models
{

    public class Expense
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Comment { get; set; }
        public User User { get; set; }
    }
}
