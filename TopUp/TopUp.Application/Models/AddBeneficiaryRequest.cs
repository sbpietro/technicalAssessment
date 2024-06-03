using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopUp.Application.Models
{
    public class AddBeneficiaryRequest
    {
        public Guid UserId { get; set; }
        [MaxLength(20)]
        public string Nickname { get; set; }
        public string PhoneNumber { get; set; }
    }
}
