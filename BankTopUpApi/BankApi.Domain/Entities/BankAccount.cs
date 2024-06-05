using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApi.Domain.Entities
{
    public class BankAccount : Entity
    {
        public Guid UserId { get; set; }
        public decimal Balance { get; set; }

        public void DebitAmountFromBalance(decimal amount)
        {
            Balance -= amount;
        }

        public void AddCreditToBalance(decimal amount)
        {
            Balance += amount;
        }
    }
}
