using BrokerBudget.Application.Common.Exceptions;
using BrokerBudget.Application.Common.Interfaces;
using MediatR;

namespace BrokerBudget.Application.UseCases.Purchases.Commands.DeletePurchase
{
    public record DeletePurchaseCommand(int Id) : IRequest;
    public class DeletePurchaseCommandHandler : IRequestHandler<DeletePurchaseCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeletePurchaseCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeletePurchaseCommand request, CancellationToken cancellationToken)
        {
            Purchase? purchase = await _context.Purchases.FindAsync(request.Id, cancellationToken);

            if (purchase is null)
                throw new NotFoundException(nameof(purchase), request.Id);

            _context.Purchases.Remove(purchase);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
