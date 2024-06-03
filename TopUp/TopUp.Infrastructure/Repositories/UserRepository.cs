﻿using TopUp.Domain;
using TopUp.Domain.Entities;
using TopUp.Domain.Interfaces;

namespace TopUp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TopUpContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public async Task AddAsync(User user)
        {
            await _context.AddAsync(user);
        }


    }
}