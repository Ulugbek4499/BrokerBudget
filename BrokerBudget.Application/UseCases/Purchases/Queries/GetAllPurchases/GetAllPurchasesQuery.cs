using AutoMapper;
using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Application.UseCases.ProductTakers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BrokerBudget.Application.UseCases.Purchases.Queries.GetAllPurchases
{
    public record GetAllPurchasesQuery : IRequest<PurchaseResponse[]>;

    public class GetAllPurchasesQueryHandler : IRequestHandler<GetAllPurchasesQuery, PurchaseResponse[]>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetAllPurchasesQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<PurchaseResponse[]> Handle(GetAllPurchasesQuery request, CancellationToken cancellationToken)
        {
            var purchases = await _context.Purchases.ToArrayAsync();

            return _mapper.Map<PurchaseResponse[]>(purchases);
        }
    }
}
