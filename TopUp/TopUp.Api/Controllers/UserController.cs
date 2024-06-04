using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TopUp.Application.Models;
using TopUp.Domain.Entities;
using TopUp.Domain.Interfaces;

namespace TopUp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null)
                return NoContent();

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserAsync(AddUserRequest request)
        {
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                IsVerified = false,
                Beneficiaries = new List<Beneficiary>()
            };

            await _userRepository.AddAsync(user);
            await _userRepository.UnitOfWork.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> ValidateUserAsync(Guid id)
        {
            //This is handled by an external service outside of this scope
            //This endpoint provides mock behavior for validating users
            var user = await _userRepository.GetByIdAsync(id);
            user.VerifyUser();

            await _userRepository.UnitOfWork.SaveChangesAsync();

            return Ok(user);
        }
    }
}
