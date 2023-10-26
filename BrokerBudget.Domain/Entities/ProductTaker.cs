using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrokerBudget.Domain.Common;

namespace BrokerBudget.Domain.Entities
{
    public class Client:BaseAuditableEntity
    {
        public string CompanyName { get; set; }
        public string ResponsiblePersonName { get; set; }
        public string PhoneNumber { get; set; }
        public string INN { get; set; }
    }
}
