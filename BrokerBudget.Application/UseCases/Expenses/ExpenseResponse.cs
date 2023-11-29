using BrokerBudget.Domain.Entities;

namespace BrokerBudget.Application.UseCases.Expenses
{
    public class ExpenseResponse
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string? Note { get; set; }
        public DateTime ExpenseDate { get; set; }

        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
