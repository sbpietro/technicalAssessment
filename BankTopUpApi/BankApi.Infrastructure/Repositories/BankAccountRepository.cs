using BankApi.Domain;
using BankApi.Domain.Entities;
using BankApi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApi.Infrastructure.Repositories
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly BankContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public BankAccountRepository(BankContext context)
        {
            _context = context;
        }

        public async Task AddAsync(BankAccount bankAccount)
        {
            await _context.BankAccounts.AddAsync(bankAccount);
        }

        public async Task<BankAccount> GetByUserIdAsync(Guid userId)
        {
            return await _context.BankAccounts.FirstOrDefaultAsync(x => x.UserId == userId);
        }

    }
}
