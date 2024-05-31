using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopUp.Domain.Entities
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsVerified { get; set; } = true;
        public List<Beneficiary> Beneficiaries { get; set; } = new();
    }
}
