using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopUp.Domain.Interfaces
{
    public interface ITopUpTransactionRepository
    {
        Task<decimal> GetAmountOnCurrentMonthAsync(Guid beneficiaryId);
        Task<decimal> GetTotalAmountPerMonthForAllBeneficiaries(Guid userId);
    }
}
