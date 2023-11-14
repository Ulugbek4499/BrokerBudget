using BrokerBudget.Domain.States;

namespace BrokerBudget.Application.UseCases.Products
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AmountCategory AmountCategory { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
