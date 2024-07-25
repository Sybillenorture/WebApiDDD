using AutoMapper;
using LuccaExpenses.Api.DTOs;
using LuccaExpenses.Api.Models;
using LuccaExpenses.Api.Repository;
using System.Text;

namespace LuccaExpenses.Api.Services
{
    public class UserService : IUserService
    {
        readonly IUserRepository _userRepository;
        readonly IMapper _mapper;
        readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, IMapper mapper, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserDto?> GetUserAsync(int userId)
        {
            User? user = await _userRepository.GetByIdAsync(userId);

            return _mapper.Map<UserDto?>(user);
        }

        public async Task<(StringBuilder, UserDto?)> ValidateUser(int userId)
        {
            StringBuilder notfoundRequestMessage = new StringBuilder();

            // Retrieve the user informations :
            // - Ensure the user exist
            // - Ensure the Expense currency match the user currency
            UserDto? user = await GetUserAsync(userId);
            if (user == null)
            {
                _logger.LogError($"User not found for userId : {userId}");
                notfoundRequestMessage.AppendLine($"User not found for userId : {userId}");
            }
            return (notfoundRequestMessage, user);
        }
    }
}
