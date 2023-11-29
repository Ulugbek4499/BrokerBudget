using BrokerBudget.Domain.Entities;

namespace BrokerBudget.Application.UseCases.ProductTakers
{
    public class ProductTakerResponse
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }
        public string? ResponsiblePersonName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? INN { get; set; }
        public string? BankAccountNumber { get; set; }

        public virtual ICollection<Purchase>? Purchases { get; set; }
        public virtual ICollection<Payment>? Payments { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
