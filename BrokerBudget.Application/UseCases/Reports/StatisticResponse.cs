using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrokerBudget.Application.UseCases.Reports
{
    public class StatisticResponse
    {
        public int CountOfAllProducts { get; set; }
        public int CountOfAllProductGivers { get; set; }
        public int CountOfAllProductTakers { get; set; }


        public decimal AmountOfAllExpenses { get; set; }
        public decimal AmountOfAllExpensesInLastYear { get; set; }
        public decimal AmountOfAllExpensesInCurrentMonth { get; set; }
        public decimal AmountOfAllExpensesInLastWeek { get; set; }
        public decimal AmountOfAllExpensesToday { get; set; }


        public Dictionary<string, decimal> AmountOfAllPaymentsByProductTakerName { get; set; }
        public decimal AmountOfAllPaymentsByProductTaker { get; set; }
        public decimal AmountOfAllPaymentsByProductTakerInLastYear { get; set; }
        public decimal AmountOfAllPaymentsByProductTakerInCurrentMonth { get; set; }
        public decimal AmountOfAllPaymentsByProductTakerInLastWeek { get; set; }
        public decimal AmountOfAllPaymentsByProductTakerToday { get; set; }


        public Dictionary<string, decimal> AmountOfAllPurchasesByProductTakerName { get; set; }
        public decimal AmountOfAllPurchasesByProductTaker { get; set; }
        public decimal AmountOfAllPurchasesByProductTakerInLastYear { get; set; }
        public decimal AmountOfAllPurchasesByProductTakerInCurrentMonth { get; set; }
        public decimal AmountOfAllPurchasesByProductTakerInLastWeek { get; set; }
        public decimal AmountOfAllPurchasesByProductTakerToday { get; set; }


        public Dictionary<string, decimal> AmountOfAllPaymentsToProductGiverName { get; set; }
        public decimal AmountOfAllPaymentsToProductGiver { get; set; }
        public decimal AmountOfAllPaymentsToProductGiverInLastYear { get; set; }
        public decimal AmountOfAllPaymentsToProductGiverInCurrentMonth { get; set; }
        public decimal AmountOfAllPaymentsToProductGiverInLastWeek { get; set; }
        public decimal AmountOfAllPaymentsToProductGiverToday { get; set; }

        public Dictionary<string, decimal> AmountOfAllPurchasesToProductGiverName { get; set; }
        public decimal AmountOfAllPurchasesToProductGiver { get; set; }
        public decimal AmountOfAllPurchasesToProductGiverInLastYear { get; set; }
        public decimal AmountOfAllPurchasesToProductGiverInCurrentMonth { get; set; }
        public decimal AmountOfAllPurchasesToProductGiverInLastWeek { get; set; }
        public decimal AmountOfAllPurchasesToProductGiverToday { get; set; }
    }
}
