using AutoMapper;
using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Application.UseCases.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BrokerBudget.Application.UseCases.Products.Queries.GetAllProducts
{
    public record GetAllProductsQuery : IRequest<ProductResponse[]>;

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, ProductResponse[]>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetAllProductsQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ProductResponse[]> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var Products = await _context.Products.ToArrayAsync();

            return _mapper.Map<ProductResponse[]>(Products);
        }
    }
}
