using BrokerBudget.Domain.Common;

namespace BrokerBudget.Domain.Entities
{
    public class Income : BaseAuditableEntity
    {
        public decimal Amount { get; set; }
        public IncomeCategory IncomeCategory { get; set; }
        public string Comment { get; set; }
        public DateTime Time { get; set; }
    }
}
