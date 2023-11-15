using AutoMapper;
using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Application.UseCases.ProductGivers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BrokerBudget.Application.UseCases.ProductGivers.Queries.GetAllProductGivers
{
    public record GetAllProductGiversQuery : IRequest<ExpenseResponse[]>;

    public class GetAllProductGiversQueryHandler : IRequestHandler<GetAllProductGiversQuery, ExpenseResponse[]>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetAllProductGiversQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ExpenseResponse[]> Handle(GetAllProductGiversQuery request, CancellationToken cancellationToken)
        {
            var ProductGivers = await _context.ProductGivers.ToArrayAsync();

            return _mapper.Map<ExpenseResponse[]>(ProductGivers);
        }
    }
}
