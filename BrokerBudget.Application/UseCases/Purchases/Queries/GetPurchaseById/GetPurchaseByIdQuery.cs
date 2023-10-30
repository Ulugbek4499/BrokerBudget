using AutoMapper;
using BrokerBudget.Application.Common.Exceptions;
using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Application.UseCases.Purchases;
using BrokerBudget.Domain.Entities;
using MediatR;

namespace BrokerBudget.Application.UseCases.Purchases.Queries.GetPurchaseById
{
    public record GetPurchaseByIdQuery(int Id) : IRequest<PurchaseResponse>;

    public class GetPurchaseByIdQueryHandler : IRequestHandler<GetPurchaseByIdQuery, PurchaseResponse>
    {
        IApplicationDbContext _dbContext;
        IMapper _mapper;

        public GetPurchaseByIdQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PurchaseResponse> Handle(GetPurchaseByIdQuery request, CancellationToken cancellationToken)
        {
            var Purchase = FilterIfPurchaseExsists(request.Id);

            var result = _mapper.Map<PurchaseResponse>(Purchase);
            return await Task.FromResult(result);
        }

        private Purchase FilterIfPurchaseExsists(int id)
            => _dbContext.Purchases
                .Find(id) ?? throw new NotFoundException(
                    " There is no Purchase with this Id. ");
    }
}
