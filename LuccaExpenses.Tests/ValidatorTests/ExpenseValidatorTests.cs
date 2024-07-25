using FluentValidation.Results;
using LuccaExpenses.Api.DTOs;
using LuccaExpenses.Api.Validators;

namespace LuccaExpenses.Tests.ValidatorTests
{
    [TestClass]
    public class ExpenseValidatorTests : BaseTests
    {
        private ExpenseValidator _validator;

        [TestInitialize]
        public void Setup()
        {
            _validator = Container.GetInstance<ExpenseValidator>();
        }

        [TestMethod]
        public void Should_Have_Error_When_Date_Is_In_The_Future()
        {
            ExpenseDto dto = new ExpenseDto { Date = DateTime.Now.AddDays(1) };
            ValidationResult result = _validator.Validate(dto);

            Assert.IsTrue(result.Errors.Any(e => e.PropertyName == "Date"));
        }

        [TestMethod]
        public void Should_Have_Error_When_Date_Is_More_Than_Three_Months_Ago()
        {
            ExpenseDto dto = new ExpenseDto { Date = DateTime.Now.AddMonths(-4) };
            ValidationResult result = _validator.Validate(dto);

            Assert.IsTrue(result.Errors.Any(e => e.PropertyName == "Date"));
        }

        [TestMethod]
        public void Should_Have_Error_When_Comment_Is_Empty()
        {
            ExpenseDto dto = new ExpenseDto { Comment = string.Empty };
            ValidationResult result = _validator.Validate(dto);

            Assert.IsTrue(result.Errors.Any(e => e.PropertyName == "Comment"));
        }

        [TestMethod]
        public void Should_Not_Have_Error_When_Expense_Is_Valid()
        {
            ExpenseDto dto = new ExpenseDto
            {
                Date = DateTime.Now.Date,
                Comment = "Valid Comment",
                Amount = 100,
                Currency = Api.Enums.Currency.USD
            };
            ValidationResult result = _validator.Validate(dto);

            Assert.IsFalse(result.Errors.Any());
        }
    }
}
