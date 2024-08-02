using Lucca.ExpenseApp.Application.UseCases;
using Lucca.ExpenseApp.Domain.Entities;
using Lucca.ExpenseApp.Dto;
using Microsoft.AspNetCore.Mvc;

namespace LuccaExpenses.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpensesController : ControllerBase
    {
        private readonly CreateExpenseUseCase _createExpenseUseCase;
        private readonly ListUserExpensesUseCase _listUserExpensesUseCase;

        public ExpensesController(CreateExpenseUseCase createExpenseUseCase, ListUserExpensesUseCase listUserExpensesUseCase)
        {
            _createExpenseUseCase = createExpenseUseCase;
            _listUserExpensesUseCase = listUserExpensesUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpense([FromBody] Expense expense)
        {
            await _createExpenseUseCase.ExecuteAsync(expense);
            return Ok(expense);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> ListUserExpenses(int userId, [FromQuery] string sortBy)
        {
            IEnumerable<ExpenseResponse> expenses = await _listUserExpensesUseCase.ExecuteAsync(userId, sortBy);
            return Ok(expenses);
        }
    }
}
