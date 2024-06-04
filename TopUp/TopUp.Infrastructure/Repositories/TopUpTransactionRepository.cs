using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopUp.Domain;
using TopUp.Domain.Interfaces;

namespace TopUp.Infrastructure.Repositories
{
    public class TopUpTransactionRepository : ITopUpTransactionRepository
    {
        private readonly TopUpContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public TopUpTransactionRepository(TopUpContext context)
        {
            _context = context;
        }

        public async Task<decimal> GetAmountOnCurrentMonthAsync(Guid beneficiaryId)
        {
            var currentAmount = _context.TopUpTransactions
                .Where(x => x.Date.Month == DateTime.UtcNow.Month
                    && x.Date.Year == DateTime.UtcNow.Year
                    && x.BeneficiaryId == beneficiaryId)
                .Sum(x => x.Amount);

            return currentAmount;
        }

        public async Task<decimal> GetTotalAmountPerMonthForAllBeneficiaries(Guid userId)
        {
            var beneficiaries = await _context.Beneficiaries.Where(x => x.UserId == userId).Select(x => x.Id).ToListAsync();
            var totalCurrent = _context.TopUpTransactions
                .Where(x => beneficiaries.Contains(x.BeneficiaryId)
                    && x.Date.Month == DateTime.UtcNow.Month
                    && x.Date.Year == DateTime.UtcNow.Year)
                .Sum(x => x.Amount);

            return totalCurrent;
        }
    }
}
