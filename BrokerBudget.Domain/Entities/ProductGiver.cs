using BrokerBudget.Domain.Common;

namespace BrokerBudget.Domain.Entities
{
    public class ProductGiver : BaseAuditableEntity
    {
        public string CompanyName { get; set; }
        public string? ResponsiblePersonName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? INN { get; set; }
        public string? BankAccountNumber { get; set; }
        public virtual ICollection<Purchase>? Purchases { get; set; }
        public virtual ICollection<Payment>? Payments { get; set; }
    }
}
