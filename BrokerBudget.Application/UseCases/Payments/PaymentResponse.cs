using BrokerBudget.Domain.Entities;

namespace BrokerBudget.Application.UseCases.Payments
{
    public class PaymentResponse
    {
        public int Id { get; set; }
        public decimal PaymentAmount { get; set; }
        public int? ProductGiverId { get; set; }
        public virtual ProductGiver? ProductGiver { get; set; }

        public int? ProductTakerId { get; set; }
        public virtual ProductTaker? ProductTaker { get; set; }

        public DateTime PaymentDate { get; set; }

        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
