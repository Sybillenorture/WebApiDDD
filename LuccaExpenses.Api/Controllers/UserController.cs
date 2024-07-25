using LuccaExpenses.Api.DTOs;
using LuccaExpenses.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace LuccaExpenses.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }


        /// <summary>
        /// Get a user given its id
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>the userDto created or errorMessage</returns>
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            try
            {

                (StringBuilder badrequestMessage, UserDto? user) = await _userService.ValidateUser(userId);
                
                if (!string.IsNullOrEmpty(badrequestMessage.ToString()))
                    return BadRequest(badrequestMessage);

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving User with Id {userId}");
                return Problem(ex.Message);
            }
        }
    }
}
