using HomeBudgetApp.Domain.Commons;
using HomeBudgetApp.Domain.States;

namespace HomeBudgetApp.Domain.Entities
{
    public class Expense : BaseAuditableEntity
    {
        public decimal Amount { get; set; }
        public ExpenseCategory ExpenseCategory { get; set; }
        public string Comment { get; set; }
        public DateTime Time { get; set; }
    }
}
