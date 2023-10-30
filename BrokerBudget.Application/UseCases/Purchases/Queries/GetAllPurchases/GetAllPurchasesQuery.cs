using AutoMapper;
using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Application.UseCases.ProductTakers;
using MediatR;

namespace BrokerBudget.Application.UseCases.Purchases.Queries.GetAllPurchases
{
    public record GetAlPurchasesQuery : IRequest<ProductTakerResponse[]>;

    public class GetAlPurchasesQueryHandler : IRequestHandler<GetAlPurchasesQuery, ProductTakerResponse[]>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetAlPurchasesQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ProductTakerResponse[]> Handle(GetAlPurchasesQuery request, CancellationToken cancellationToken)
        {
            var ProductTakers = await _context.ProductTakers.ToArrayAsync();

            return _mapper.Map<ProductTakerResponse[]>(ProductTakers);
        }
    }
}
