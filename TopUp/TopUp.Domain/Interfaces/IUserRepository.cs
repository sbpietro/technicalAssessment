using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopUp.Domain.Entities;

namespace TopUp.Domain.Interfaces
{
    public interface IUserRepository
    {
        public IUnitOfWork UnitOfWork { get; }
        Task AddAsync(User user);
        Task<User> GetByIdAsync(Guid id);
    }
}
