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

        public decimal AmountOfAllPaymentsByProductTaker { get; set; }
        public decimal AmountOfAllPaymentsByProductTakerInLastYear { get; set; }
        public decimal AmountOfAllPaymentsByProductTakerInLastSixMonth { get; set; }
        public decimal AmountOfAllPaymentsByProductTakerInLastMonth { get; set; }
        public decimal AmountOfAllPaymentsByProductTakerInLastWeek { get; set; }
        public decimal AmountOfAllPaymentsByProductTakerToday { get; set; }

        public decimal AmountOfAllPurchasesByProductTaker { get; set; }
        public decimal AmountOfAllPurchasesByProductTakerInLastYear { get; set; }
        public decimal AmountOfAllPurchasesByProductTakerInLastSixMonth { get; set; }
        public decimal AmountOfAllPurchasesByProductTakerInLastMonth { get; set; }
        public decimal AmountOfAllPurchasesByProductTakerInLastWeek { get; set; }
        public decimal AmountOfAllPurchasesByProductTakerToday { get; set; }

        public decimal AmountOfAllPaymentsToProductGiver { get; set; }
        public decimal AmountOfAllPaymentsToProductGiverInLastYear { get; set; }
        public decimal AmountOfAllPaymentsToProductGiverInLastSixMonth { get; set; }
        public decimal AmountOfAllPaymentsToProductGiverInLastMonth { get; set; }
        public decimal AmountOfAllPaymentsToProductGiverInLastWeek { get; set; }
        public decimal AmountOfAllPaymentsToProductGiverToday { get; set; }

        public decimal AmountOfAllPurchasesToProductGiver { get; set; }
        public decimal AmountOfAllPurchasesToProductGiverInLastYear { get; set; }
        public decimal AmountOfAllPurchasesToProductGiverInLastSixMonth { get; set; }
        public decimal AmountOfAllPurchasesToProductGiverInLastMonth { get; set; }
        public decimal AmountOfAllPurchasesToProductGiverInLastWeek { get; set; }
        public decimal AmountOfAllPurchasesToProductGiverToday { get; set; }



    }
}
