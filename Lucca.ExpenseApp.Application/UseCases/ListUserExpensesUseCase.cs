using Lucca.ExpenseApp.Application.Services.Interfaces;
using Lucca.ExpenseApp.Dto;

namespace Lucca.ExpenseApp.Application.UseCases
{
    public class ListUserExpensesUseCase
    {
        private readonly IExpenseService _expenseService;

        public ListUserExpensesUseCase(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        public async Task<IEnumerable<ExpenseResponse>> ExecuteAsync(int userId, string sortBy)
        {
            return await _expenseService.ListUserExpensesAsync(userId, sortBy);
        }
    }
}