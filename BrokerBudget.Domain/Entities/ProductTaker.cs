using BrokerBudget.Domain.Common;

namespace BrokerBudget.Domain.Entities
{
    public class ProductTaker:BaseAuditableEntity
    {
        public string CompanyName { get; set; }
        public string ResponsiblePersonName { get; set; }
        public string PhoneNumber { get; set; }
        public string INN { get; set; }

        public virtual ICollection<Purchase>? Purchases { get; set; }
    }
}
