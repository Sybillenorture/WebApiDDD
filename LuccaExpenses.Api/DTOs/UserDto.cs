using LuccaExpenses.Api.Enums;
using System.Text.Json.Serialization;

namespace LuccaExpenses.Api.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Currency Currency { get; set; }
    }
}
