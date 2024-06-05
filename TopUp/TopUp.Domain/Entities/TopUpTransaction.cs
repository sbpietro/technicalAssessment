using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopUp.Domain.Entities
{
    public class TopUpTransaction : Entity
    {
        public Guid BeneficiaryId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public virtual Beneficiary Beneficiary { get; set;}

    }
}
