using BrokerBudget.Application.Common.Exceptions;
using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Domain.Entities;
using MediatR;

namespace BrokerBudget.Application.UseCases.Products.Commands.DeleteProduct
{
    public record DeleteProductCommand(int Id) : IRequest;
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            Product? Product = await _context.Products.FindAsync(request.Id, cancellationToken);

            if (Product is null)
                throw new NotFoundException(nameof(Product), request.Id);

            _context.Products.Remove(Product);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
