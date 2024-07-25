using AutoMapper;
using LuccaExpenses.Api.DTOs;
using LuccaExpenses.Api.Enums;
using LuccaExpenses.Api.MappingProfiles;
using LuccaExpenses.Api.Models;

namespace LuccaExpenses.Tests.MappingTests
{
    [TestClass]
    public class ExpenseMappingProfileTests : BaseTests
    {
        private readonly IMapper _mapper;

        public ExpenseMappingProfileTests()
        {
            _mapper = Container.GetInstance<IMapper>();
        }

        [TestMethod]
        public void Should_Map_Expense_To_ExpenseDto()
        {
            // Arrange
            var user = new User { FirstName = "John", LastName = "Doe" };
            var expense = new Expense
            {
                Id = 1,
                UserId = 1,
                Date = DateTime.UtcNow,
                Type = "Hotel",
                Amount = 100.50m,
                Currency = "USD",
                Comment = "Business Lunch",
                User = user
            };

            // Act
            var expenseDto = _mapper.Map<ExpenseDto>(expense);

            // Assert
            Assert.AreEqual(expense.Id, expenseDto.Id);
            Assert.AreEqual(expense.UserId, expenseDto.UserId);
            Assert.AreEqual($"{expense.User.FirstName} {expense.User.LastName}", expenseDto.UserName);
            Assert.AreEqual(expense.Date, expenseDto.Date);
            Assert.AreEqual(expense.Type, expenseDto.Type.ToString());
            Assert.AreEqual(expense.Amount, expenseDto.Amount);
            Assert.AreEqual(expense.Currency, expenseDto.Currency.ToString());
            Assert.AreEqual(expense.Comment, expenseDto.Comment);
        }

        [TestMethod]
        public void Should_Map_ExpenseDto_To_Expense()
        {
            // Arrange
            var expenseDto = new ExpenseDto
            {
                Id = 1,
                UserId = 1,
                Date = DateTime.UtcNow,
                Type = ExpenseType.Misc,
                Amount = 100.50m,
                Currency = Currency.USD,
                Comment = "Business Lunch",
                UserName = "John Doe"
            };

            // Act
            var expense = _mapper.Map<Expense>(expenseDto);

            // Assert
            Assert.AreEqual(expenseDto.Id, expense.Id);
            Assert.AreEqual(expenseDto.UserId, expense.UserId);
            Assert.AreEqual(expenseDto.Date, expense.Date);
            Assert.AreEqual(expenseDto.Type.ToString(), expense.Type);
            Assert.AreEqual(expenseDto.Amount, expense.Amount);
            Assert.AreEqual(expenseDto.Currency.ToString(), expense.Currency);
            Assert.AreEqual(expenseDto.Comment, expense.Comment);
        }
    }
}
