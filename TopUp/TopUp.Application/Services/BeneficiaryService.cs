using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopUp.Application.Models;
using TopUp.Domain.Entities;
using TopUp.Domain.Interfaces;

namespace TopUp.Application.Services
{
    public interface IBeneficiaryService
    {
        Task AddBeneficiaryAsync(AddBeneficiaryRequest request);
        Task<List<Beneficiary>> GetAllByUserId(Guid userId);
    }

    public class BeneficiaryService : IBeneficiaryService
    {
        private readonly IBeneficiaryRepository _repository;

        public BeneficiaryService(IBeneficiaryRepository beneficiaryRepository)
        {
            _repository = beneficiaryRepository;
        }

        public async Task<List<Beneficiary>> GetAllByUserId(Guid userId)
        {
            var beneficiaries = await _repository.GetAllByUserIdAsync(userId);

            return beneficiaries;
        }

        public async Task AddBeneficiaryAsync(AddBeneficiaryRequest request)
        {
            var currentBeneficiariesCount = await _repository.GetBeneficiaryCountByUserAsync(request.UserId);

            if (currentBeneficiariesCount == 5)
                throw new ApiApplicationException("Unable to add any more beneficiaries. Already reached maximmum (5)");

            var beneficiary = new Beneficiary()
            {
                UserId = request.UserId,
                Nickname = request.Nickname,
                PhoneNumber = request.PhoneNumber,
                TopUpBalance = 0
            };

            await _repository.AddAsync(beneficiary);
            await _repository.UnitOfWork.SaveChangesAsync();
        }
    }
}
