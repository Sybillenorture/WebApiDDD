using AutoMapper;
using Lucca.ExpenseApp.Application.Services.Interfaces;
using Lucca.ExpenseApp.Domain.Entities;
using Lucca.ExpenseApp.Domain.Interfaces.Repositories;
using Lucca.ExpenseApp.Dto;

namespace Lucca.ExpenseApp.Application.Services.Concrete
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IClaimantRepository _claimantRepository;
        private readonly IMapper _mapper;

        public ExpenseService(IExpenseRepository expenseRepository, IClaimantRepository claimantRepository, IMapper mapper)
        {
            _expenseRepository = expenseRepository;
            _claimantRepository = claimantRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ExpenseResponse>> ListUserExpensesAsync(int userId, string sortBy)
        {
            var expenses = await _expenseRepository.GetClaimantExpensesAsync(userId);
            var expenseResponses = _mapper.Map<IEnumerable<ExpenseResponse>>(expenses);

            // Optionnel : Ajoutez du tri basé sur le paramètre sortBy si nécessaire
            return sortBy switch
            {
                "amount" => expenseResponses.OrderBy(e => e.Amount),
                "date" => expenseResponses.OrderBy(e => e.Date),
                _ => expenseResponses
            };
        }

        public async Task<ExpenseResponse> AddAsync(Expense expense)
        {
            // Ajouter la dépense au repository
            await _expenseRepository.AddAsync(expense);

            // Récupérer les dépenses mises à jour pour le claimant
            var expenses = await _expenseRepository.GetClaimantExpensesAsync(expense.ClaimantId);

            // Mapper les dépenses en ExpenseResponse
            var expenseResponses = _mapper.Map<IEnumerable<ExpenseResponse>>(expenses);

            // Retourner la dernière dépense ajoutée
            var addedExpenseResponse = expenseResponses
                .OrderByDescending(e => e.Date) // Vous pouvez ajuster le critère de tri ici si nécessaire
                .FirstOrDefault(e => e.Id == expense.Id);

            return addedExpenseResponse;
        }
    }
}
