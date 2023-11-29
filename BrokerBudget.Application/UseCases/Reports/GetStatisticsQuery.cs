using AutoMapper;
using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BrokerBudget.Application.UseCases.Reports
{
    public record GetStatisticsQuery : IRequest<StatisticResponse>;

    public class GetStatisticsQueryHandler : IRequestHandler<GetStatisticsQuery, StatisticResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetStatisticsQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<StatisticResponse> Handle(GetStatisticsQuery request, CancellationToken cancellationToken)
        {
            var currentDate = DateTime.Now;
            var currentYear = currentDate.Year;

            var expenses = await _context.Expenses.ToArrayAsync();
            var productTakers = await _context.ProductTakers.ToArrayAsync();
            var productGivers = await _context.ProductGivers.ToArrayAsync();
            var payments = await _context.Payments.ToArrayAsync();
            var purchases = await _context.Purchases.ToArrayAsync();
            var products = await _context.Products.ToArrayAsync();

            var productTakerPaymentsWithNameAndTotalPaymentsPair = _context.ProductTakers
                 .GroupJoin(
                     _context.Payments,
                     productTaker => productTaker.Id,
                     payment => payment.ProductTakerId,
                     (productTaker, payments) => new
                     {
                         CompanyName = productTaker.CompanyName,
                         TotalPayments = payments.Sum(payment => payment.PaymentAmount)
                     })
                 .ToDictionary(result => result.CompanyName, result => result.TotalPayments);

            var productTakerPurchasesWithNameAndTotalPaymentsPair = _context.ProductTakers
                 .GroupJoin(
                     _context.Purchases,
                     productTaker => productTaker.Id,
                     purchase => purchase.ProductTakerId,
                     (productTaker, purchases) => new
                     {
                         CompanyName = productTaker.CompanyName,
                         TotalPurchases = purchases.Sum(purchase => purchase.FinalPriceOfPurchase)??0
                     })
                 .ToDictionary(result => result.CompanyName, result => result.TotalPurchases);

            var productGiverPaymentsWithNameAndTotalPaymentsPair = _context.ProductGivers
                 .GroupJoin(
                     _context.Payments,
                     productGiver => productGiver.Id,
                     payment => payment.ProductGiverId,
                     (productGiver, payments) => new
                     {
                         CompanyName = productGiver.CompanyName,
                         TotalPayments = payments.Sum(payment => payment.PaymentAmount)
                     })
                 .ToDictionary(result => result.CompanyName, result => result.TotalPayments);

            var productGiverPurchasesWithNameAndTotalPaymentsPair = _context.ProductGivers
                 .GroupJoin(
                     _context.Purchases,
                     productGiver => productGiver.Id,
                     purchase => purchase.ProductGiverId,
                     (productGiver, purchases) => new
                     {
                         CompanyName = productGiver.CompanyName,
                         TotalPurchases = purchases.Sum(purchase => purchase.FinalPriceOfPurchase) ?? 0
                     })
                 .ToDictionary(result => result.CompanyName, result => result.TotalPurchases);


            var statistic = new StatisticResponse
            {
                CountOfAllProducts = products.Length,
                CountOfAllProductGivers = productGivers.Length,
                CountOfAllProductTakers = productTakers.Length,


                AmountOfAllExpenses = expenses.Sum(e => e.Amount),
                AmountOfAllExpensesInLastYear = expenses
                    .Where(e => e.ExpenseDate.Year == currentYear)
                    .Sum(e => e.Amount),
                AmountOfAllExpensesInCurrentMonth = expenses
                    .Where(e => e.ExpenseDate.Month == currentDate.Month)
                    .Sum(e => e.Amount),
                AmountOfAllExpensesInLastWeek = expenses
                     .Where(c => (currentDate - c.ExpenseDate).TotalDays <= 7)
                     .Sum(c => c.Amount),
                AmountOfAllExpensesToday = expenses
                     .Where(c => c.ExpenseDate.Date == currentDate.Date)
                     .Sum(c => c.Amount),


                AmountOfAllPaymentsByProductTakerName = productTakerPaymentsWithNameAndTotalPaymentsPair,
                AmountOfAllPaymentsByProductTaker = payments
                     .Where(p => p.ProductTakerId != null || p.ProductTakerId != 0)
                     .Sum(p => p.PaymentAmount),
                AmountOfAllPaymentsByProductTakerInLastYear = payments
                     .Where(p => p.PaymentDate.Year == currentYear && (p.ProductTakerId != null || p.ProductTakerId != 0))
                     .Sum(p => p.PaymentAmount),
                AmountOfAllPaymentsByProductTakerInCurrentMonth = payments
                     .Where(p => p.PaymentDate.Month == currentDate.Month && (p.ProductTakerId != null || p.ProductTakerId != 0))
                     .Sum(p => p.PaymentAmount),
                AmountOfAllPaymentsByProductTakerInLastWeek = payments
                     .Where(p => (currentDate - p.PaymentDate).TotalDays <= 7 && (p.ProductTakerId != null || p.ProductTakerId != 0))
                     .Sum(p => p.PaymentAmount),
                AmountOfAllPaymentsByProductTakerToday = payments
                     .Where(p => p.PaymentDate.Date == currentDate.Date && (p.ProductTakerId != null || p.ProductTakerId != 0))
                     .Sum(p => p.PaymentAmount),


                AmountOfAllPurchasesByProductTakerName = productTakerPurchasesWithNameAndTotalPaymentsPair,
                AmountOfAllPurchasesByProductTaker = purchases
                     .Where(p => p.ProductTakerId != null || p.ProductTakerId != 0)
                     .Sum(p => p.FinalPriceOfPurchase)??0,
                AmountOfAllPurchasesByProductTakerInLastYear = purchases
                     .Where(p => p.PurchaseDate.Year == currentYear && (p.ProductTakerId != null || p.ProductTakerId != 0))
                     .Sum(p => p.FinalPriceOfPurchase) ?? 0,
                AmountOfAllPurchasesByProductTakerInCurrentMonth = purchases
                     .Where(p => p.PurchaseDate.Month == currentDate.Month && (p.ProductTakerId != null || p.ProductTakerId != 0))
                     .Sum(p => p.FinalPriceOfPurchase) ?? 0,
                AmountOfAllPurchasesByProductTakerInLastWeek = purchases
                     .Where(p => (currentDate - p.PurchaseDate).TotalDays <= 7 && (p.ProductTakerId != null || p.ProductTakerId != 0))
                     .Sum(p => p.FinalPriceOfPurchase) ?? 0,
                AmountOfAllPurchasesByProductTakerToday = purchases
                     .Where(p => p.PurchaseDate.Date == currentDate.Date && (p.ProductTakerId != null || p.ProductTakerId != 0))
                     .Sum(p => p.FinalPriceOfPurchase) ?? 0,


                AmountOfAllPaymentsToProductGiverName = productGiverPaymentsWithNameAndTotalPaymentsPair,
                AmountOfAllPaymentsToProductGiver = payments
                     .Where(p => p.ProductGiverId != null || p.ProductGiverId != 0)
                     .Sum(p => p.PaymentAmount),
                AmountOfAllPaymentsToProductGiverInLastYear = payments
                     .Where(p => p.PaymentDate.Year == currentYear && (p.ProductGiverId != null || p.ProductGiverId != 0))
                     .Sum(p => p.PaymentAmount),
                AmountOfAllPaymentsToProductGiverInCurrentMonth = payments
                     .Where(p => p.PaymentDate.Month == currentDate.Month && (p.ProductGiverId != null || p.ProductGiverId != 0))
                     .Sum(p => p.PaymentAmount),
                AmountOfAllPaymentsToProductGiverInLastWeek = payments
                     .Where(p => (currentDate - p.PaymentDate).TotalDays <= 7 && (p.ProductGiverId != null || p.ProductGiverId != 0))
                     .Sum(p => p.PaymentAmount),
                AmountOfAllPaymentsToProductGiverToday = payments
                     .Where(p => p.PaymentDate.Date == currentDate.Date && (p.ProductGiverId != null || p.ProductGiverId != 0))
                     .Sum(p => p.PaymentAmount),


                AmountOfAllPurchasesToProductGiverName = productGiverPurchasesWithNameAndTotalPaymentsPair,
                AmountOfAllPurchasesToProductGiver = purchases
                     .Where(p => p.ProductTakerId != null || p.ProductTakerId != 0)
                     .Sum(p => p.FinalPriceOfPurchase) ?? 0,
                AmountOfAllPurchasesToProductGiverInLastYear = purchases
                     .Where(p => p.PurchaseDate.Year == currentYear && (p.ProductTakerId != null || p.ProductTakerId != 0))
                     .Sum(p => p.FinalPriceOfPurchase) ?? 0,
                AmountOfAllPurchasesToProductGiverInCurrentMonth = purchases
                     .Where(p => p.PurchaseDate.Month == currentDate.Month && (p.ProductTakerId != null || p.ProductTakerId != 0))
                     .Sum(p => p.FinalPriceOfPurchase) ?? 0,
                AmountOfAllPurchasesToProductGiverInLastWeek = purchases
                     .Where(p => (currentDate - p.PurchaseDate).TotalDays <= 7 && (p.ProductTakerId != null || p.ProductTakerId != 0))
                     .Sum(p => p.FinalPriceOfPurchase) ?? 0,
                AmountOfAllPurchasesToProductGiverToday = purchases
                     .Where(p => p.PurchaseDate.Date == currentDate.Date && (p.ProductTakerId != null || p.ProductTakerId != 0))
                     .Sum(p => p.FinalPriceOfPurchase) ?? 0,
            };

            return statistic;
        }
    }
}
