using AutoMapper;
using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Application.UseCases.ProductTakers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BrokerBudget.Application.UseCases.Purchases.Queries.GetAllPurchases
{
    public record GetAllPurchasesQuery : IRequest<ProductTakerResponse[]>;

    public class GetAllPurchasesQueryHandler : IRequestHandler<GetAllPurchasesQuery, ProductTakerResponse[]>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetAllPurchasesQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ProductTakerResponse[]> Handle(GetAllPurchasesQuery request, CancellationToken cancellationToken)
        {
            var ProductTakers = await _context.ProductTakers.ToArrayAsync();

            return _mapper.Map<ProductTakerResponse[]>(ProductTakers);
        }
    }
}
