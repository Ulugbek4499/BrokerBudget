using BrokerBudget.Application.Common.Exceptions;
using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Domain.Entities;
using MediatR;

namespace BrokerBudget.Application.UseCases.ProductTakers.Commands.DeleteProductTaker
{

    public record DeleteProductTakerCommand(int Id) : IRequest;
    public class DeleteProductTakerCommandHandler : IRequestHandler<DeleteProductTakerCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteProductTakerCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteProductTakerCommand request, CancellationToken cancellationToken)
        {
            ProductTaker? productTaker = await _context.ProductTakers.FindAsync(request.Id, cancellationToken);

            if (productTaker is null)
                throw new NotFoundException(nameof(productTaker), request.Id);

            _context.ProductTakers.Remove(productTaker);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
