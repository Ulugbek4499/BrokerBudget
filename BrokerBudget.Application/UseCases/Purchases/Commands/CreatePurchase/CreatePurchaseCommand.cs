﻿using AutoMapper;
using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Domain.Entities;
using MediatR;

namespace BrokerBudget.Application.UseCases.Purchases.Commands.CreatePurchase
{
    public class CreatePurchaseCommand : IRequest<int>
    {
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

    public class CreatePurchaseCommandHandler : IRequestHandler<CreatePurchaseCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public CreatePurchaseCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<int> Handle(CreatePurchaseCommand request, CancellationToken cancellationToken)
        {
            Purchase Purchase = _mapper.Map<Purchase>(request);

            decimal saleAmountCategoryPercentage = request.SaleAmountCategoryPercentage ?? 0;
            decimal saleForTotalPrice = request.SaleForTotalPrice ?? 0;

            Purchase.FinalPriceOfPurchase =
                (Purchase.Amount - saleAmountCategoryPercentage)
                    * Purchase.PricePerAmount - saleForTotalPrice;

            await _context.Purchases.AddAsync(Purchase, cancellationToken);
            await _context.SaveChangesAsync();

            return Purchase.Id;
        }
    }
}
