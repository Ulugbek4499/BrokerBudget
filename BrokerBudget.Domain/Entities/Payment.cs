using BrokerBudget.Domain.Common;

namespace BrokerBudget.Domain.Entities
{
    public class Payment:BaseAuditableEntity
    {
        public int? ProductGiverId { get; set; }
        public virtual ProductGiver? ProductGiver { get; set; }

        public int? ProductTakerId { get; set; }
        public virtual ProductTaker? ProductTaker { get; set; }

        public DateTime PaymentDate { get; set; }
    }
}
