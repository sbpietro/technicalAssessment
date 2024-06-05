using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TopUp.Application.Models;
using TopUp.Domain.Entities;
using TopUp.Domain.Interfaces;

namespace TopUp.Application.Services
{
    public interface ITransactionService
    {
        Task CreateTopUpTransactionAsync(CreateTopUpTransactionRequest request);
    }

    public class TransactionService : ITransactionService
    {
        private readonly IBeneficiaryRepository _beneficiaryRepository;
        private readonly ITopUpTransactionRepository _topUpTransactionRepository;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        //TODO: Add http client to external bank service
        public TransactionService(
            IBeneficiaryRepository beneficiaryRepository,
            ITopUpTransactionRepository topUpTransactionRepository,
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _beneficiaryRepository = beneficiaryRepository;
            _topUpTransactionRepository = topUpTransactionRepository;
            _httpClient = httpClient;
            _configuration = configuration;

            _httpClient.BaseAddress = new Uri(_configuration["BankApiEndpoint"]);
        }

        public async Task CreateTopUpTransactionAsync(CreateTopUpTransactionRequest request)
        {
            var beneficiary = await _beneficiaryRepository.GetByIdAsync(request.BeneficiaryId);
            var bankAccount = await GetBankAccountData(beneficiary.UserId);
            var balance = bankAccount.Balance;

            if(request.Amount > balance)
                throw new ApiApplicationException("Cannot top up this amount. This exceeds your current account balance.");

            var totalCurrent = await _topUpTransactionRepository.GetTotalAmountPerMonthForAllBeneficiaries(beneficiary.UserId);

            if (totalCurrent >= 3000m)
                throw new ApiApplicationException("Cannot exceed limit of 3000 AED per month. Try again next month.");

            var amountOnCurrentMonth = await _topUpTransactionRepository.GetAmountOnCurrentMonthAsync(request.BeneficiaryId);

            if (!beneficiary.User.IsVerified)
            {
                if (amountOnCurrentMonth >= 1000m)
                    throw new ApiApplicationException("Cannot top up any more this month for the current beneficiary. Try again next month.");
            }
            else
            {
                if (amountOnCurrentMonth >= 500m)
                    throw new ApiApplicationException("Cannot top up any more this month for the current beneficiary. Try again next month.");
            }

            var transactionCreated = await CreateTransactionOnBankApi(request.Amount, beneficiary.UserId);
            if (!transactionCreated)
                throw new ApiApplicationException("Transaction was not created successfully. Please try again later.");

            var transaction = new TopUpTransaction()
            {
                Amount = request.Amount,
                BeneficiaryId = beneficiary.Id,
                Date = DateTime.UtcNow
            };

            await _topUpTransactionRepository.AddAsync(transaction);
            await _topUpTransactionRepository.UnitOfWork.SaveChangesAsync();

            beneficiary.TopUpBalance += request.Amount;
            await _beneficiaryRepository.UnitOfWork.SaveChangesAsync();


        }

        private async Task<BankAccountResponse> GetBankAccountData(Guid userId)
        {
            var response = await _httpClient.GetAsync($"bankAccount/balance/{userId}");
            var content = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            var bankAccount = JsonSerializer.Deserialize<BankAccountResponse>(content);
            return bankAccount;
        }

        private async Task<bool> CreateTransactionOnBankApi(decimal amount, Guid userId)
        {
            
            var request = new {  Amount = amount, UserId = userId };
            //var jsonContent = JsonSerializer.Serialize(request);

            var response = await _httpClient.PostAsJsonAsync($"bankAccount/topUp", request);
            if (!response.IsSuccessStatusCode)
                return false;

            return true;
        }


    }
}
