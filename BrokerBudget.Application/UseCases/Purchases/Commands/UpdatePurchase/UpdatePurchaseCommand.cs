using AutoMapper;
using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Domain.Entities;
using MediatR;

namespace BrokerBudget.Application.UseCases.Purchases.Commands.UpdatePurchase
{
    public class UpdatePurchaseCommand : IRequest
    {
        public int Id { get; set; }
        public int Count { get; set; } = 1;
        public int CardId { get; set; }
        public int GameId { get; set; }
        public string UserId { get; set; }
    }

    public class UpdatePurchaseCommandHandler : IRequestHandler<UpdatePurchaseCommand>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public UpdatePurchaseCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task Handle(UpdatePurchaseCommand request, CancellationToken cancellationToken)
        {
            Purchase? purchase = await _context.Purchases.FindAsync(request.Id);
            _mapper.Map(request, purchase);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
