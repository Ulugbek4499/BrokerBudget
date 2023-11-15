using AutoMapper;
using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Application.UseCases.Expenses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BrokerBudget.Application.UseCases.Expenses.Queries.GetAllExpenses
{
    public record GetAllExpensesQuery : IRequest<ExpenseResponse[]>;

    public class GetAllExpensesQueryHandler : IRequestHandler<GetAllExpensesQuery, ExpenseResponse[]>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetAllExpensesQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ExpenseResponse[]> Handle(GetAllExpensesQuery request, CancellationToken cancellationToken)
        {
            var Expenses = await _context.Expenses.ToArrayAsync();

            return _mapper.Map<ExpenseResponse[]>(Expenses);
        }
    }
}
