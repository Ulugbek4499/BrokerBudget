using AutoMapper;
using BrokerBudget.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BrokerBudget.Application.UseCases.ProductGivers.Queries.GetAllProductGivers
{
    public record GetAllProductGiversQuery : IRequest<ProductGiverResponse[]>;

    public class GetAllProductGiversQueryHandler : IRequestHandler<GetAllProductGiversQuery, ProductGiverResponse[]>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetAllProductGiversQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ProductGiverResponse[]> Handle(GetAllProductGiversQuery request, CancellationToken cancellationToken)
        {
            var ProductGivers = await _context.ProductGivers.ToArrayAsync();

            return _mapper.Map<ProductGiverResponse[]>(ProductGivers);
        }
    }
}
