using AutoMapper;
using FluentValidation;
using LuccaExpenses.Api.Controllers;
using LuccaExpenses.Api.DTOs;
using LuccaExpenses.Api.Models;
using LuccaExpenses.Api.Repository;
using System.Text;

namespace LuccaExpenses.Api.Services
{
    public class ExpenseService : IExpenseService
    {
        readonly IExpenseRepository _expenseRepository;
        readonly IUserService _userService;
        readonly IValidator<ExpenseDto> _validator;
        readonly IMapper _mapper;
        readonly ILogger<ExpenseService> _logger;


        public ExpenseService(IExpenseRepository expenseRepository, IUserService userService, IValidator<ExpenseDto> validator, IMapper mapper, ILogger<ExpenseService> logger)
        {
            _expenseRepository = expenseRepository;
            _userService = userService;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Expense> CreateExpenseAsync(ExpenseDto expenseDto)
        {      
            Expense expense = _mapper.Map<Expense>(expenseDto);

            await _expenseRepository.AddAsync(expense);
            return expense;
        }

        public async Task<IEnumerable<ExpenseDto>> GetExpensesAsync(int userId, string sortBy)
        {
            List<Expense> expenses = await _expenseRepository.GetByUserIdAsync(userId);
            UserDto user = await _userService.GetUserAsync(userId);

            IEnumerable<ExpenseDto> sortedExpenses = _mapper.Map<IEnumerable<ExpenseDto>>(expenses);

            switch (sortBy.ToLower())
            {
                case "amount":
                    sortedExpenses.OrderBy(e => e.Amount);
                    break;
                case "date":
                    sortedExpenses.OrderBy(e => e.Date);
                    break;
                default:
                    break;
            }

            return sortedExpenses;
        }

        public async Task<string> ValidateExpense(ExpenseDto expenseDto)
        {
            StringBuilder badRequestMessage = new StringBuilder();

            // Retrieve the user informations :
            // - Ensure the user exist
            // - Ensure the Expense currency match the user currency
            (badRequestMessage, UserDto? user) = await _userService.ValidateUser(expenseDto.UserId);
            if (user?.Currency != expenseDto.Currency)
            {
                _logger.LogError($"User's currency ({user?.Currency}) does not match the expense's currency ({expenseDto.UserId})");
                badRequestMessage.AppendLine("Expense currency does not match user's currency");
            }

            // check if the Expense have already been created
            IEnumerable<ExpenseDto>? expensesFound = await GetDuplicatedExpenses(expenseDto);
            if (expensesFound != null && expensesFound.Any())
            {
                _logger.LogError($"An expense already exist for the specified userId {expenseDto.UserId}", expensesFound);
                badRequestMessage.AppendLine($"Expense already exist for the specified userId {expenseDto.UserId}");
            }

            var validationResult = await _validator.ValidateAsync(expenseDto);
            if (!validationResult.IsValid)
            {
                _logger.LogError("The Expense you tried to create does not match the rules",
                                 validationResult.Errors.ToArray());
                validationResult.Errors.ForEach(error => badRequestMessage.AppendLine(error.ErrorMessage));
            }

            return badRequestMessage.ToString();
        }

        /// <summary>
        /// check if the Expense have already been created
        /// </summary>
        /// <param name="expenseDto">given an ExpenseDto</param>
        /// <returns>Collection of duplicated ExpenseDto</returns>
        public async Task<IEnumerable<ExpenseDto>> GetDuplicatedExpenses (ExpenseDto expenseDto)
        {
            IEnumerable<Expense> expensesFound = await _expenseRepository.GetDuplicatedExpenseRecords(expenseDto.UserId, expenseDto.Date, expenseDto.Amount);

            return _mapper.Map<IEnumerable<ExpenseDto>>(expensesFound);
        }

        
    }
}
