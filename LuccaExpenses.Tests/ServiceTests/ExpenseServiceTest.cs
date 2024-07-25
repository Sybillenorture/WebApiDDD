using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using LuccaExpenses.Api.DTOs;
using LuccaExpenses.Api.Enums;
using LuccaExpenses.Api.Models;
using LuccaExpenses.Api.Repository;
using LuccaExpenses.Api.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace LuccaExpenses.Tests.ServiceTests
{
    [TestClass]
    public class ExpenseServiceTests : BaseTests
    {
        private IExpenseService _expenseService;
        private LuccaExpensesDbContext _context;
        private IExpenseRepository _expenseRepository;
        private IUserService _userService;
        private IValidator<ExpenseDto> _validator;
        private IMapper _mapper;
        private ILogger<ExpenseService> _logger;

        [TestInitialize]
        public void Setup()
        {
            // Utiliser le conteneur pour obtenir les instances des services nécessaires
            _expenseService = Container.GetInstance<IExpenseService>();
            _context = Container.GetInstance<LuccaExpensesDbContext>();
            _expenseRepository = Container.GetInstance<IExpenseRepository>();
            _userService = Container.GetInstance<IUserService>();
            _validator = Container.GetInstance<IValidator<ExpenseDto>>();
            _mapper = Container.GetInstance<IMapper>();
            _logger = Container.GetInstance<ILogger<ExpenseService>>();

            _context.Expense.AddRange(
                new Expense { Id = 1, UserId = 1, Amount = 100, Comment = "Expense Test 1" , Type = "Hotel", Currency = "USD", Date = DateTime.Today.AddDays(-1) },
                new Expense { Id = 2, UserId = 2, Amount = 200, Comment = "Expense Test 2", Type = "Hotel", Currency = "RUB", Date = DateTime.Today.AddDays(-2) }
            );

            _context.SaveChanges();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Expense.RemoveRange(_context.Expense);
            _context.SaveChanges();
        }

        [TestMethod]
        public async Task CreateExpenseAsync_ShouldCreateExpense_WhenValid()
        {
            // Arrange
            ExpenseDto expenseDto = new ExpenseDto { UserId = 1, Amount = 150, Comment = "Expense Test 3", Type = ExpenseType.Restaurant, Currency = Currency.USD, Date = DateTime.Today };

            // Act
            Expense result = await _expenseService.CreateExpenseAsync(expenseDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expenseDto.UserId, result.UserId);
            Assert.AreEqual(expenseDto.Amount, result.Amount);
            Assert.AreEqual(expenseDto.Currency.ToString(), result.Currency);
            Assert.AreEqual(expenseDto.Date, result.Date);
        }

        [TestMethod]
        public async Task GetExpensesAsync_ShouldReturnSortedExpenses_WhenValid()
        {
            // Arrange
            int userId = 1;
            string sortBy = "amount";

            // Act
            IEnumerable<ExpenseDto> expenseDtos = await _expenseService.GetExpensesAsync(userId, sortBy);

            // Assert
            Assert.IsNotNull(expenseDtos);
            Assert.IsTrue(expenseDtos.Any());
            Assert.AreEqual(1, expenseDtos.First().UserId);
        }

        [TestMethod]
        public async Task ValidateExpense_ShouldReturnErrorMessage_WhenCurrencyMismatch()
        {
            // Arrange
            ExpenseDto expenseDto = new ExpenseDto { UserId = 2, Amount = 50, Comment = "Expense Test 4", Type = ExpenseType.Misc, Currency = Currency.EUR, Date = DateTime.Today };

            // Act
            string badRequestErrorMessage = await _expenseService.ValidateExpense(expenseDto);

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(badRequestErrorMessage));
            Assert.IsTrue(badRequestErrorMessage.Contains("Expense currency does not match user's currency"));
        }

        [TestMethod]
        public async Task ValidateExpense_ShouldReturnErrorMessage_WhenExpenseAlreadyExists()
        {
            // Arrange
            ExpenseDto expenseDto = new ExpenseDto { UserId = 1, Amount = 100, Comment = "Expense Test 5", Type = ExpenseType.Hotel, Currency = Currency.USD, Date = DateTime.Today.AddDays(-1) };

            // Act
            string badRequestErrorMessage = await _expenseService.ValidateExpense(expenseDto);

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(badRequestErrorMessage));
            Assert.IsTrue(badRequestErrorMessage.Contains($"Expense already exist for the specified userId {expenseDto.UserId}"));
        }

        [TestMethod]
        public async Task ValidateExpense_ShouldReturnErrorMessage_WhenValidationFails()
        {
            // Arrange
            ExpenseDto expenseDto = new ExpenseDto { UserId = 2, Amount = -50, Comment = "Expense Test", Type = ExpenseType.Restaurant, Currency = Currency.RUB, Date = DateTime.Today };

            // Act
            string badRequestErrorMessage = await _expenseService.ValidateExpense(expenseDto);

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(badRequestErrorMessage));
            Assert.IsTrue(badRequestErrorMessage.Contains("The amount must be greater than zero.\r\n"));
        }
    }
}