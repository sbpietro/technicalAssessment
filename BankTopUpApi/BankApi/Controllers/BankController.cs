using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        public BankController()
        {
                
        }

        [HttpGet]
        public async Task<IActionResult> GetBalance()
        {
            return Ok();
        }

        [HttpPost]
        [Route("charge")]
        public async Task<IActionResult> ChargeTopUpTransaction()
        {
            return Ok();
        }

        [HttpPut]
        [Route("credit")]
        public async Task<IActionResult> AddCreditToAccount()
        {
            return Ok();
        }

        //[HttpPut]
        //[Route("debit")]
        //public async Task<IActionResult> DebitFromAccount()
        //{

        //}
    }
}
