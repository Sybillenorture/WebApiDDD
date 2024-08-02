using AutoMapper;
using FluentValidation;
using Lamar;
using Lucca.ExpenseApp.Application.MappingProfiles;
using Lucca.ExpenseApp.Application.Services.Concrete;
using Lucca.ExpenseApp.Application.Services.Interfaces;
using Lucca.ExpenseApp.Application.UseCases;
using Lucca.ExpenseApp.Application.Validators;
using Lucca.ExpenseApp.Domain.Entities;
using Lucca.ExpenseApp.Domain.Interfaces.Repositories;
using Lucca.ExpenseApp.Infrastructure.Repositories;

namespace Lucca.ExpenseApp.Api
{
    public class IoCRegistry : ServiceRegistry
    {
        public IoCRegistry() {

            // Register services and repositories using Lamar
            For<IExpenseRepository>().Use<ExpenseRepository>();
            For<IClaimantRepository>().Use<ClaimantRepository>();
            For<CreateExpenseUseCase>().Use<CreateExpenseUseCase>();
            For<ListUserExpensesUseCase>().Use<ListUserExpensesUseCase>();
            For<IExpenseValidationService>().Use<ExpenseValidationService>();
            For<IClaimantService>().Use<ClaimantService>();
            For<IExpenseService>().Use<ExpenseService>();

            // Register FluentValidation validators
            For<IValidator<Expense>>().Use<ExpenseValidator>();
            For<IValidator<Claimant>>().Use<ClaimantValidator>();

            For<IMapper>().Use(ctx => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ClaimantMappingProfile>();
                cfg.AddProfile<ExpenseMappingProfile>();
            }).CreateMapper());
        }
    }
}
