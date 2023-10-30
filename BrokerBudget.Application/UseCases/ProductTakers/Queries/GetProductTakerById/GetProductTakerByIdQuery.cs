using AutoMapper;
using BrokerBudget.Application.Common.Exceptions;
using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Domain.Entities;
using MediatR;

namespace BrokerBudget.Application.UseCases.ProductTakers.Queries.GetProductTakerTakerById
{
    public record GetProductTakerByIdQuery(int Id) : IRequest<ProductTakerResponse>;

    public class GetProductTakerByIdQueryHandler : IRequestHandler<GetProductTakerByIdQuery, ProductTakerResponse>
    {
        IApplicationDbContext _dbContext;
        IMapper _mapper;

        public GetProductTakerByIdQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ProductTakerResponse> Handle(GetProductTakerByIdQuery request, CancellationToken cancellationToken)
        {
            var ProductTaker = FilterIfProductTakerExsists(request.Id);

            var result = _mapper.Map<ProductTakerResponse>(ProductTaker);
            return await Task.FromResult(result);
        }

        private ProductTaker FilterIfProductTakerExsists(int id)
            => _dbContext.ProductTakers
                .Find(id) ?? throw new NotFoundException(
                    " There is no ProductTaker with this Id. ");
    }
}
