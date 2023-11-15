using AutoMapper;
using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Domain.Entities;
using MediatR;

namespace BrokerBudget.Application.UseCases.Expenses.Commands.CreateExpense
{
    public class CreateExpenseCommand : IRequest<int>
    {
        public decimal Amount { get; set; }
        public string Note { get; set; }
        public DateTime ExpenseDate { get; set; }
    }

    public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public CreateExpenseCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<int> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
        {
            Expense Expense = _mapper.Map<Expense>(request);
            await _context.Expenses.AddAsync(Expense, cancellationToken);
            await _context.SaveChangesAsync();

            return Expense.Id;
        }
    }
}
