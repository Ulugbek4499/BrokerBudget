using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrokerBudget.Domain.Entities
{
    public class Payment
    {
        public int? ProductGiverId { get; set; }
        public virtual ProductGiver? ProductGiver { get; set; }

        public int? ProductTakerId { get; set; }
        public virtual ProductTaker? ProductTaker { get; set; }

        public DateTime PaymentDate { get; set; }
    }
}
