using Microsoft.EntityFrameworkCore;
using TopUp.Domain;
using TopUp.Domain.Entities;
using TopUp.Domain.Interfaces;

namespace TopUp.Infrastructure.Repositories
{
    public class BeneficiaryRepository : IBeneficiaryRepository
    {
        private readonly TopUpContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public async Task AddAsync(Beneficiary beneficiary)
        {
            await _context.AddAsync(beneficiary);
        }

        public async Task<int> GetBeneficiaryCountByUserAsync(Guid userId)
        {
            return await _context.Beneficiaries
                .Where(x => x.UserId == userId)
                .CountAsync();
        }

        public async Task<List<Beneficiary>> GetAllByUserIdAsync(Guid userId)
        {
            return await _context.Beneficiaries
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }


    }
}
