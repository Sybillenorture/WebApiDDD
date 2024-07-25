using LuccaExpenses.Api.DTOs;
using LuccaExpenses.Api.Models;

namespace LuccaExpenses.Api.Repository
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int userId);
    }
}
