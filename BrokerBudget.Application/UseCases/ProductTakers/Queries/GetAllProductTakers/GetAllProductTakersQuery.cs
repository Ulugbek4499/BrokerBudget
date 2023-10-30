using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Application.UseCases.ProductTakers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BrokerBudget.Application.UseCases.ProductTakerTakers.Queries.GetAllProductTakerTakers
{
    public record GetAllProductTakersQuery : IRequest<ProductTakerResponse[]>;

    public class GetAllProductTakersQueryHandler : IRequestHandler<GetAllProductTakersQuery, ProductTakerResponse[]>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetAllProductTakersQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ProductTakerResponse[]> Handle(GetAllProductTakersQuery request, CancellationToken cancellationToken)
        {
            var ProductTakers = await _context.ProductTakers.ToArrayAsync();

            return _mapper.Map<ProductTakerResponse[]>(ProductTakers);
        }
    }
}
