using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopUp.Application.Models;
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
        private readonly IUserRepository _userRepository;
        private readonly ITopUpTransactionRepository _topUpTransactionRepository;

        //TODO: Add http client to external bank service
        public TransactionService(
            IBeneficiaryRepository beneficiaryRepository,
            IUserRepository userRepository,
            ITopUpTransactionRepository topUpTransactionRepository)
        {
            _beneficiaryRepository = beneficiaryRepository;
            _userRepository = userRepository;
            _topUpTransactionRepository = topUpTransactionRepository;
        }

        public async Task CreateTopUpTransactionAsync(CreateTopUpTransactionRequest request)
        {
            var beneficiary = await _beneficiaryRepository.GetByIdAsync(request.BeneficiaryId);
            var balance = 0m; //TODO: Get from bank service

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

        }
    }
}
