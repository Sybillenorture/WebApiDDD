

using LuccaExpenses.Api.DTOs;
using System.Text;

namespace LuccaExpenses.Api.Services
{
    public interface IUserService
    {
        Task<UserDto?> GetUserAsync(int userId);
        Task<(StringBuilder, UserDto?)> ValidateUser(int userId);
    }
}
