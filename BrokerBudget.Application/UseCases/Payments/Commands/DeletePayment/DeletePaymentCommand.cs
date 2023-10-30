using BrokerBudget.Application.Common.Exceptions;
using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Domain.Entities;
using MediatR;

namespace BrokerBudget.Application.UseCases.Payments.Commands.DeletePayment
{
    public record DeletePaymentCommand(int Id) : IRequest;
    public class DeletePaymentCommandHandler : IRequestHandler<DeletePaymentCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeletePaymentCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
        {
            Payment? payment = await _context.Payments.FindAsync(request.Id, cancellationToken);

            if (payment is null)
                throw new NotFoundException(nameof(payment), request.Id);

            _context.Payments.Remove(payment);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
