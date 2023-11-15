using AutoMapper;
using BrokerBudget.Application.Common.Exceptions;
using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Application.UseCases.ProductGivers;
using BrokerBudget.Domain.Entities;
using MediatR;

namespace BrokerBudget.Application.UseCases.ProductGivers.Queries.GetProductGiverById
{
    public record GetProductGiverByIdQuery(int Id) : IRequest<ExpenseResponse>;

    public class GetProductGiverByIdQueryHandler : IRequestHandler<GetProductGiverByIdQuery, ExpenseResponse>
    {
        IApplicationDbContext _dbContext;
        IMapper _mapper;

        public GetProductGiverByIdQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ExpenseResponse> Handle(GetProductGiverByIdQuery request, CancellationToken cancellationToken)
        {
            var ProductGiver = FilterIfProductGiverExsists(request.Id);

            var result = _mapper.Map<ExpenseResponse>(ProductGiver);
            return await Task.FromResult(result);
        }

        private ProductGiver FilterIfProductGiverExsists(int id)
            => _dbContext.ProductGivers
                .Find(id) ?? throw new NotFoundException(
                    " There is no ProductGiver with this Id. ");
    }
}
