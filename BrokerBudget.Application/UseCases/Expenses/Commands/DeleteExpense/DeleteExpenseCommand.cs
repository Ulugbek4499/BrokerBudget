using BrokerBudget.Application.Common.Exceptions;
using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Domain.Entities;
using MediatR;

namespace BrokerBudget.Application.UseCases.Expenses.Commands.DeleteExpense
{
    public record DeleteExpenseCommand(int Id) : IRequest;
    public class DeleteExpenseCommandHandler : IRequestHandler<DeleteExpenseCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteExpenseCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
        {
            Expense? Expense = await _context.Expenses.FindAsync(request.Id, cancellationToken);

            if (Expense is null)
                throw new NotFoundException(nameof(Expense), request.Id);

            _context.Expenses.Remove(Expense);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
