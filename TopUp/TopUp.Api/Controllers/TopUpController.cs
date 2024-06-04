using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TopUp.Application;

namespace TopUp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopUpController : ControllerBase
    {
        public TopUpController()
        {
            
        }

        [HttpGet]
        public async Task<IActionResult> GetTopOptionsAsync()
        {
            return Ok(Enum.GetValues<TopUpOptionsEnum>());
        }


    }
}
