using Lucca.ExpenseApp.Application.Services.Concrete;
using Lucca.ExpenseApp.Domain.Entities;
using Lucca.ExpenseApp.Domain.Interfaces.Repositories;
using Lucca.ExpenseApp.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Lucca.ExpenseApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClaimantController : ControllerBase
    {
        private readonly IClaimantRepository _claimantRepository;


        private readonly ClaimantService _claimantService;

        public ClaimantController(ClaimantService claimantService)
        {
            _claimantService = claimantService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GeClaimantById(int id)
        {
            var claimantResponse = await _claimantService.GetByIdAsync(id);
            if (claimantResponse == null)
            {
                return NotFound(new { Message = $"Claimant with ID {id} not found." });
            }

            return Ok(claimantResponse);
        }
    }
}
