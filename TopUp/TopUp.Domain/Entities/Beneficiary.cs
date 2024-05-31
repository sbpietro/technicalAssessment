using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopUp.Domain.Entities
{
    public class Beneficiary : Entity
    {
        [MaxLength(20)]
        public string Nickname { get; set; }
        public string PhoneNumber { get; set; }
        public decimal TopUpBalance { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
