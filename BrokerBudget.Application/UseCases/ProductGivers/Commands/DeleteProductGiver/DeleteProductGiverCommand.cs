using BrokerBudget.Application.Common.Exceptions;
using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Domain.Entities;
using MediatR;

namespace BrokerBudget.Application.UseCases.ProductGivers.Commands.DeleteProductGiver
{
    public record DeleteProductGiverCommand(int Id) : IRequest;
    public class DeleteProductGiverCommandHandler : IRequestHandler<DeleteProductGiverCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteProductGiverCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteProductGiverCommand request, CancellationToken cancellationToken)
        {
            ProductGiver? productGiver = await _context.ProductGivers.FindAsync(request.Id, cancellationToken);

            if (productGiver is null)
                throw new NotFoundException(nameof(productGiver), request.Id);

            _context.ProductGivers.Remove(productGiver);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
