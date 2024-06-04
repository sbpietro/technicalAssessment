using BankApi.Application.Models;
using BankApi.Domain.Entities;
using BankApi.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IBankAccountRepository _bankAccountRepository;

        public BankController(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }

        [HttpGet]
        [Route("balance/{userId}")]
        public async Task<IActionResult> GetBalance(Guid userId)
        {
            return Ok(await _bankAccountRepository.GetByUserIdAsync(userId));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAccount(TopUpChargeRequest request)
        {
            var accountExists = await _bankAccountRepository.GetByUserIdAsync(request.UserId);

            if (accountExists is null)
                return BadRequest("Account for requested user already exists.");

            var bankAccount = new BankAccount()
            {
                UserId = request.UserId,
                Balance = request.Amount
            };

            await _bankAccountRepository.AddAsync(bankAccount);
            await _bankAccountRepository.UnitOfWork.SaveChangesAsync();

            return Ok(bankAccount);
        }

        [HttpPost]
        [Route("charge")]
        public async Task<IActionResult> ChargeTopUpTransaction(TopUpChargeRequest request)
        {
            var bankAccount = await _bankAccountRepository.GetByUserIdAsync(request.UserId);
            //Adds 1 dollar charge to transaction amount
            request.Amount++;

            bankAccount.DebitAmountFromBalance(request.Amount);
            await _bankAccountRepository.UnitOfWork.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [Route("credit")]
        public async Task<IActionResult> AddCreditToAccount(TopUpChargeRequest request)
        {
            var bankAccount = await _bankAccountRepository.GetByUserIdAsync(request.UserId);
            bankAccount.AddCreditToBalance(request.Amount);
            await _bankAccountRepository.UnitOfWork.SaveChangesAsync();
            return Ok();
        }

        
    }
}
