using LuccaExpenses.Api.DTOs;
using LuccaExpenses.Api.Models;
using LuccaExpenses.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;

namespace LuccaExpenses.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;
        private readonly IUserService _userService;
        private readonly ILogger<ExpenseController> _logger;

        public ExpenseController(IExpenseService expenseService, IUserService userService ,ILogger<ExpenseController> logger)
        {
            _expenseService = expenseService;
            _logger = logger;
            _userService = userService;
        }

        /// <summary>
        /// Create an expense given an expenseDto and required data
        /// </summary>
        /// <param name="dto">ExpenseDto</param>
        /// <returns>status code with expenseDto created or error message</returns>
        [HttpPost]
        public async Task<IActionResult> CreateExpense([FromBody] ExpenseDto dto)
        {
            try
            {
                string prerequestValidation = await _expenseService.ValidateExpense(dto);
                if (!string.IsNullOrEmpty(prerequestValidation))
                {
                    return BadRequest(prerequestValidation);
                }

                Expense expense = await _expenseService.CreateExpenseAsync(dto);
                return Ok(expense);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating an expense");
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// Get the expenses given a User. By default sort the expenses by date.
        /// Expenses can be sorted also by amount
        /// </summary>
        /// <param name="userId">id of the given user</param>
        /// <param name="sortBy">amount or date allowed</param>
        /// <returns>status code with expenseDtos retrieved or error message</returns>
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetExpenses(int userId, [FromQuery] string sortBy = "date")
        {
            try
            {
                (StringBuilder badrequestMessage,UserDto? user) = await _userService.ValidateUser(userId);
                if(!string.IsNullOrEmpty(badrequestMessage.ToString()))
                    return BadRequest(badrequestMessage.ToString());

                IEnumerable<ExpenseDto> expenses = await _expenseService.GetExpensesAsync(userId, sortBy);
                List<ExpenseDto> dto = expenses.ToList();
                return Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving expenses for userId : {userId}");
                return Problem(ex.Message);
            }  
        }
    }
}
