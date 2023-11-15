using AutoMapper;
using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Domain.Entities;
using MediatR;

namespace BrokerBudget.Application.UseCases.Expenses.Commands.UpdateExpense
{
    public class UpdateExpenseCommand : IRequest
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Note { get; set; }
        public DateTime ExpenseDate { get; set; }
    }

    public class UpdateExpenseCommandHandler : IRequestHandler<UpdateExpenseCommand>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public UpdateExpenseCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
        {
            Expense? Expense = await _context.Expenses.FindAsync(request.Id);
            _mapper.Map(request, Expense);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
