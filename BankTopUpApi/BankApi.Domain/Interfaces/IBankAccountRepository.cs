using BankApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApi.Domain.Interfaces
{
    public interface IBankAccountRepository
    {
        public IUnitOfWork UnitOfWork { get; }
        Task AddAsync(BankAccount bankAccount);
        Task<BankAccount> GetByUserIdAsync(Guid userId);

    }
}
