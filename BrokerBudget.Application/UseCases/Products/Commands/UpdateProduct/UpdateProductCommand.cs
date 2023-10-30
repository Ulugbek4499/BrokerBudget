using AutoMapper;
using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Domain.Entities;
using MediatR;

namespace BrokerBudget.Application.UseCases.Products.Commands.UpdateProduct
{

    public class UpdateProductCommand : IRequest
    {
        public int Id { get; set; }
        public int Count { get; set; } = 1;
        public int CardId { get; set; }
        public int GameId { get; set; }
        public string UserId { get; set; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public UpdateProductCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            Product? product = await _context.Products.FindAsync(request.Id);
            _mapper.Map(request, product);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
