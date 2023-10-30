using AutoMapper;
using BrokerBudget.Application.Common.Exceptions;
using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Application.UseCases.Products;
using BrokerBudget.Domain.Entities;
using MediatR;

namespace BrokerBudget.Application.UseCases.Products.Queries.GetProductById
{
    public record GetProductByIdQuery(int Id) : IRequest<ProductResponse>;

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductResponse>
    {
        IApplicationDbContext _dbContext;
        IMapper _mapper;

        public GetProductByIdQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var Product = FilterIfProductExsists(request.Id);

            var result = _mapper.Map<ProductResponse>(Product);
            return await Task.FromResult(result);
        }

        private Product FilterIfProductExsists(int id)
            => _dbContext.Products
                .Find(id) ?? throw new NotFoundException(
                    " There is no Product with this Id. ");
    }
}
