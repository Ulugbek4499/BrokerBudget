using AutoMapper;
using BrokerBudget.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BrokerBudget.Application.UseCases.Payments.Queries.GetAllPayments
{

    public record GetAllPaymentsQuery : IRequest<PaymentResponse[]>;

    public class GetAllPaymentsQueryHandler : IRequestHandler<GetAllPaymentsQuery, PaymentResponse[]>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetAllPaymentsQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<PaymentResponse[]> Handle(GetAllPaymentsQuery request, CancellationToken cancellationToken)
        {
            var Payments = await _context.Payments.ToArrayAsync();

            return _mapper.Map<PaymentResponse[]>(Payments);
        }
    }
}
