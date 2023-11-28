using AutoMapper;
using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Domain.Entities;
using MediatR;

namespace BrokerBudget.Application.UseCases.Purchases.Commands.UpdatePurchase
{
    public class UpdatePurchaseCommand : IRequest
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal Amount { get; set; }
        public decimal? SaleAmountCategoryPercentage { get; set; }

        public decimal PricePerAmount { get; set; }
        public decimal? SaleForTotalPrice { get; set; }
        public decimal? TakenMoneyAmount { get; set; }
        public DateTime PurchaseDate { get; set; }

        public int? ProductGiverId { get; set; }
        public int? ProductTakerId { get; set; }
    }

    public class UpdatePurchaseCommandHandler : IRequestHandler<UpdatePurchaseCommand>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public UpdatePurchaseCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task Handle(UpdatePurchaseCommand request, CancellationToken cancellationToken)
        {
            Purchase? purchase = await _context.Purchases.FindAsync(request.Id);

            purchase.FinalPriceOfPurchase =
              ((purchase.Amount - purchase.SaleAmountCategoryPercentage)
                  * purchase.PricePerAmount - purchase.SaleForTotalPrice) ?? 0;

            _mapper.Map(request, purchase);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
