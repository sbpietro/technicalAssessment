using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopUp.Domain.Entities;

namespace TopUp.Domain.Interfaces
{
    public interface ITopUpTransactionRepository
    {
        public IUnitOfWork UnitOfWork { get; }
        Task AddAsync(TopUpTransaction topUpTransaction);
        Task<decimal> GetAmountOnCurrentMonthAsync(Guid beneficiaryId);
        Task<decimal> GetTotalAmountPerMonthForAllBeneficiaries(Guid userId);
    }
}
