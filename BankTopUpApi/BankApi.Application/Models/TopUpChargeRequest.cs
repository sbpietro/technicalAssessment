using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApi.Application.Models
{
    public class TopUpChargeRequest
    {
        public Guid UserId {  get; set; }   
        public decimal Amount {  get; set; }
    }
}
