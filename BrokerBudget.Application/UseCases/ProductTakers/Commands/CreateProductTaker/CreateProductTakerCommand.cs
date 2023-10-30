using AutoMapper;
using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Domain.Entities;
using MediatR;

namespace BrokerBudget.Application.UseCases.ProductTakers.Commands.CreateProductTaker
{
    public class CreateProductTakerCommand : IRequest<int>
    {
        public string UserId { get; set; }
    }

    public class CreateProductTakerCommandHandler : IRequestHandler<CreateProductTakerCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public CreateProductTakerCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<int> Handle(CreateProductTakerCommand request, CancellationToken cancellationToken)
        {
            ProductTaker productTaker = _mapper.Map<ProductTaker>(request);
            await _context.ProductTakers.AddAsync(productTaker, cancellationToken);
            await _context.SaveChangesAsync();

            return productTaker.Id;
        }
    }
}
