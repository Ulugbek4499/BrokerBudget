using AutoMapper;
using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Domain.Entities;
using MediatR;

namespace BrokerBudget.Application.UseCases.ProductGivers.Commands.CreateProductGiver
{
    public class CreateProductGiverCommand : IRequest<int>
    {
        public string UserId { get; set; }
    }

    public class CreateProductGiverCommandHandler : IRequestHandler<CreateProductGiverCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public CreateProductGiverCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<int> Handle(CreateProductGiverCommand request, CancellationToken cancellationToken)
        {
            ProductGiver productGiver = _mapper.Map<ProductGiver>(request);
            await _context.ProductGivers.AddAsync(productGiver, cancellationToken);
            await _context.SaveChangesAsync();

            return productGiver.Id;
        }
    }
}
