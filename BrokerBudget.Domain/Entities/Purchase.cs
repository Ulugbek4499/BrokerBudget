using BrokerBudget.Domain.Common;

namespace BrokerBudget.Domain.Entities
{
    public class Purchase:BaseAuditableEntity
    {
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public decimal Amount { get; set; }
        public decimal? SaleAmountCategoryPercentage { get; set; }

        public decimal PricePerAmount { get; set; }
        public decimal? SaleForTotalPrice { get; set; }

        public int? ProductGiverId { get; set; }
        public virtual ProductGiver? ProductGiver { get; set; }

        public int? ProductTakerId { get; set; }
        public virtual ProductTaker? ProductTaker { get; set; }
    }
}