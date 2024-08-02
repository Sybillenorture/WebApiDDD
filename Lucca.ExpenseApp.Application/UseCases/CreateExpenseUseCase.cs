using AutoMapper;
using Lucca.ExpenseApp.Application.Services.Interfaces;
using Lucca.ExpenseApp.Domain.Entities;
using Lucca.ExpenseApp.Domain.Interfaces.Repositories;
using Lucca.ExpenseApp.Dto;

namespace Lucca.ExpenseApp.Application.UseCases
{
    public class CreateExpenseUseCase
    {
        private readonly IExpenseService _expenseService;
        private readonly IClaimantService _claimantService;
        private readonly IExpenseValidationService _validationService;
        private readonly IMapper _mapper;

        public CreateExpenseUseCase(IExpenseService expenseService, IClaimantService claimantService, IExpenseValidationService validationService, IMapper mapper)
        {
            _expenseService = expenseService;
            _claimantService = claimantService;
            _validationService = validationService;
            _mapper = mapper;
        }

        public async Task<ExpenseResponse> ExecuteAsync(Expense expense)
        {
            var claimant = await _claimantService.GetByIdAsync(expense.ClaimantId);
            if (claimant == null) throw new Exception("Claimant not found");

            expense.Claimant = _mapper.Map<Claimant>(claimant);

            await _validationService.ValidateAsync(expense);
            
           return await _expenseService.AddAsync(expense);
        }
    }
}