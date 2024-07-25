using LuccaExpenses.Api.Enums;
using System.Text.Json.Serialization;

namespace LuccaExpenses.Api.DTOs
{
    public class ExpenseDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [JsonPropertyName("owner")]
        public string? UserName { get; set; }

        public DateTime Date { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ExpenseType Type { get; set; }

        public decimal Amount { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Currency Currency { get; set; }

        public string? Comment { get; set; }
    }
}
