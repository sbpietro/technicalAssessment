using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TopUp.Application;
using TopUp.Application.Models;
using TopUp.Application.Services;
using TopUp.Domain;

namespace TopUp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopUpController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TopUpController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        [Route("options")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTopOptionsAsync()
        {
            return Ok(Enum.GetValues<TopUpOptionsEnum>());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTopUpTransaction(CreateTopUpTransactionRequest request)
        {
            await _transactionService.CreateTopUpTransactionAsync(request);
            return Ok();
        }


    }
}
