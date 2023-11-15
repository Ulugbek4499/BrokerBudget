using AutoMapper;
using BrokerBudget.Application.Common.Exceptions;
using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Application.UseCases.Expenses;
using BrokerBudget.Domain.Entities;
using MediatR;

namespace BrokerBudget.Application.UseCases.Expenses.Queries.GetExpenseById
{
    public record GetExpenseByIdQuery(int Id) : IRequest<ExpenseResponse>;

    public class GetExpenseByIdQueryHandler : IRequestHandler<GetExpenseByIdQuery, ExpenseResponse>
    {
        IApplicationDbContext _dbContext;
        IMapper _mapper;

        public GetExpenseByIdQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ExpenseResponse> Handle(GetExpenseByIdQuery request, CancellationToken cancellationToken)
        {
            var Expense = FilterIfExpenseExsists(request.Id);

            var result = _mapper.Map<ExpenseResponse>(Expense);
            return await Task.FromResult(result);
        }

        private Expense FilterIfExpenseExsists(int id)
            => _dbContext.Expenses
                .Find(id) ?? throw new NotFoundException(
                    " There is no Expense with this Id. ");
    }
}
