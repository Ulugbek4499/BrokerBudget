using BrokerBudget.Domain.Common;

namespace BrokerBudget.Domain.Entities
{
    public class ProductOwner:BaseAuditableEntity
    {
        public string CompanyName { get; set; }
        public string ResponsiblePersonName { get; set; }
        public string INN { get; set; }
    }
}
