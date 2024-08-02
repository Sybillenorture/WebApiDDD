using FluentValidation;
using Lucca.ExpenseApp.Application.Services.Concrete;
using Lucca.ExpenseApp.Application.Services.Interfaces;
using Lucca.ExpenseApp.Application.UseCases;
using Lucca.ExpenseApp.Domain.Entities;
using Lucca.ExpenseApp.Domain.Interfaces.Repositories;

namespace LuccaExpenses.Tests
{
    [TestClass]
    public class CreateExpenseUseCaseTests : BaseTests
    {
        private IExpenseRepository _expenseRepository;
        private IClaimantRepository _claimantRepository;
        private IExpenseValidationService _expenseValidationService;
        private CreateExpenseUseCase _createExpenseUseCase;
        private ListUserExpensesUseCase _listUserExpensesUseCase;

        [TestInitialize]
        public void Setup()
        {
            _expenseRepository = Container.GetInstance<IExpenseRepository>();
            _claimantRepository = Container.GetInstance<IClaimantRepository>();
            _expenseValidationService = Container.GetInstance<IExpenseValidationService>();
            _createExpenseUseCase = Container.GetInstance<CreateExpenseUseCase>();
            _listUserExpensesUseCase = Container.GetInstance<ListUserExpensesUseCase>();

            Assert.IsNotNull(_expenseRepository);
            Assert.IsNotNull(_claimantRepository);
            Assert.IsNotNull(_expenseValidationService);
            Assert.IsNotNull(_createExpenseUseCase);
            Assert.IsNotNull(_listUserExpensesUseCase);

        }

        [TestMethod]
        public async Task ExecuteAsync_ShouldAddExpense_WhenValid()
        {
            // Arrange
            var expense = new Expense
            {
                ClaimantId = 1,
                Date = DateTime.Today,
                Amount = 100,
                Currency = "USD",
                Comment = "Dinner"
            };

            // Act
            await _createExpenseUseCase.ExecuteAsync(expense);

            // Assert
            var savedExpense = await _listUserExpensesUseCase.ExecuteAsync(expense.ClaimantId, "Date");
            Assert.IsNotNull(savedExpense);
            Assert.AreEqual(expense.Amount, savedExpense?.First().Amount);
        }

        [TestMethod]
        public async Task ExecuteAsync_ShouldThrowValidationException_WhenExpenseCurrencyDoesNotMatchUser()
        {
            // Arrange
            var expense = new Expense
            {
                ClaimantId = 1,
                Date = DateTime.Today,
                Amount = 100,
                Currency = "EUR", // Mismatch with user's currency
                Comment = "Dinner"
            };

            // Act & Assert
            var exception = await Assert.ThrowsExceptionAsync<ValidationException>(
                async () => await _createExpenseUseCase.ExecuteAsync(expense)
            );

            Assert.AreEqual("Expense currency must match user's currency", exception.Message);
        }

        [TestMethod]
        public async Task ExecuteAsync_ShouldThrowException_WhenUserNotFound()
        {
            // Arrange
            var expense = new Expense
            {
                ClaimantId = 999, // Non-existent user ID
                Date = DateTime.Today,
                Amount = 100,
                Currency = "USD",
                Comment = "Dinner"
            };

            // Act & Assert
            var exception = await Assert.ThrowsExceptionAsync<Exception>(
                async () => await _createExpenseUseCase.ExecuteAsync(expense)
            );

            Assert.AreEqual("Claimant not found", exception.Message);
        }
    }
}