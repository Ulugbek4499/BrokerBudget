using AutoMapper;
using BrokerBudget.Application.Common.Interfaces;
using MediatR;

namespace BrokerBudget.Application.UseCases.Payments.Commands.CreatePayment
{

    public class CreatePaymentCommand : IRequest<int>
    {
        public string UserId { get; set; }
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.OnSale;
    }

    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public CreatePaymentCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<int> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            Payment payment = _mapper.Map<Payment>(request);
            await _context.Payments.AddAsync(payment, cancellationToken);
            await _context.SaveChangesAsync();

            return payment.Id;
        }
    }
}
