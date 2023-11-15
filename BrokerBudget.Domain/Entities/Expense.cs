using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrokerBudget.Domain.Common;

namespace BrokerBudget.Domain.Entities
{
    public class Expense:BaseAuditableEntity
    {
        public decimal Amount { get; set; }
        public string Note { get; set; }
        public DateTime ExpenseDate { get; set; }
    }
}
