using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopUp.Domain.Entities;

namespace TopUp.Domain.Interfaces
{
    public interface IBeneficiaryRepository
    {
        public IUnitOfWork UnitOfWork { get; }
        Task<int> GetBeneficiaryCountByUserAsync(Guid userId);
        Task AddAsync(Beneficiary beneficiary);
        Task<List<Beneficiary>> GetAllByUserIdAsync(Guid userId);
    }
}
