using AutoMapper;
using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Domain.Entities;
using BrokerBudget.Domain.States;
using MediatR;

namespace BrokerBudget.Application.UseCases.Products.Commands.CreateProduct
{

    public class CreateProductCommand : IRequest<int>
    {
        public string Name { get; set; }
        public AmountCategory AmountCategory { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public CreateProductCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = _mapper.Map<Product>(request);
            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync();

            return product.Id;
        }
    }
}
