using BrokerBudget.Domain.Entities;

namespace BrokerBudget.Application.UseCases.Purchases
{
    public class PurchaseResponse
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public decimal Amount { get; set; }
        public decimal? SaleAmountCategoryPercentage { get; set; }

        public decimal PricePerAmount { get; set; }
        public decimal? SaleForTotalPrice { get; set; }

        public DateTime PurchaseDate { get; set; }

        public int? ProductGiverId { get; set; }
        public virtual ProductGiver? ProductGiver { get; set; }

        public int? ProductTakerId { get; set; }
        public virtual ProductTaker? ProductTaker { get; set; }

        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
