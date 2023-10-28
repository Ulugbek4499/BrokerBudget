using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrokerBudget.Domain.Common;
using BrokerBudget.Domain.States;

namespace BrokerBudget.Domain.Entities
{
    public class Product:BaseAuditableEntity
    {
        public string Name { get; set; }
        public AmountCategory AmountCategory { get; set; }
    }
}
