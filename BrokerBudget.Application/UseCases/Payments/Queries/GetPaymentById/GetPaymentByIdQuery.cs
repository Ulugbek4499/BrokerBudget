using AutoMapper;
using BrokerBudget.Application.Common.Exceptions;
using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Domain.Entities;
using MediatR;

namespace BrokerBudget.Application.UseCases.Payments.Queries.GetPaymentById
{
    public record GetPaymentByIdQuery(int Id) : IRequest<PaymentResponse>;

    public class GetPaymentByIdQueryHandler : IRequestHandler<GetPaymentByIdQuery, PaymentResponse>
    {
        IApplicationDbContext _dbContext;
        IMapper _mapper;

        public GetPaymentByIdQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PaymentResponse> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
        {
            var Payment = FilterIfPaymentExsists(request.Id);

            var result = _mapper.Map<PaymentResponse>(Payment);
            return await Task.FromResult(result);
        }

        private Payment FilterIfPaymentExsists(int id)
            => _dbContext.Payments
                .Find(id) ?? throw new NotFoundException(
                    " There is no Payment with this Id. ");
    }
}
