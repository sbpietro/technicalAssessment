using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TopUp.Application.Models;
using TopUp.Application.Services;

namespace TopUp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficiaryController : ControllerBase
    {
        private readonly IBeneficiaryService _beneficiaryService;

        public BeneficiaryController(IBeneficiaryService beneficiaryService)
        {
            _beneficiaryService = beneficiaryService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("userId/{id}")]
        public async Task<IActionResult> ViewBeneficiariesByUser(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("User ID must be informed");

            var beneficiaries = await _beneficiaryService.GetAllByUserId(id);
            return Ok(beneficiaries);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddBeneficiary(AddBeneficiaryRequest request)
        {
            await _beneficiaryService.AddBeneficiaryAsync(request);
            return Created();
        }
    }
}
