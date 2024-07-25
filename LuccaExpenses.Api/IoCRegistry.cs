using FluentValidation;
using Lamar;
using LuccaExpenses.Api.DTOs;
using LuccaExpenses.Api.Repository;
using LuccaExpenses.Api.Services;
using LuccaExpenses.Api.Validators;

namespace LuccaExpenses.Api
{
    public class IoCRegistry : ServiceRegistry
    {
        public IoCRegistry() {

            // Register services and repositories using Lamar
            For<IUserRepository>().Use<UserRepository>();
            For<IExpenseRepository>().Use<ExpenseRepository>();
            For<IUserService>().Use<UserService>();
            For<IExpenseService>().Use<ExpenseService>();
            For<IValidator<ExpenseDto>>().Use<ExpenseValidator>();
        }
    }
}
