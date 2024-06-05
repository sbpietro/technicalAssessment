using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using TopUp.Domain;
using TopUp.Domain.Entities;
using TopUp.Domain.Interfaces;

namespace TopUp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TopUpContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public UserRepository(TopUpContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            await _context.AddAsync(user);
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

    }
}
