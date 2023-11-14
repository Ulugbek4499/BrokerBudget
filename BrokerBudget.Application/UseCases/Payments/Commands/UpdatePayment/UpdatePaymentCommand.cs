using AutoMapper;
using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Domain.Entities;
using MediatR;

namespace BrokerBudget.Application.UseCases.Payments.Commands.UpdatePayment
{

    public class UpdatePaymentCommand : IRequest
    {
        public int Id { get; set; }
        public decimal PaymentAmount { get; set; }
        public int? ProductGiverId { get; set; }
        public int? ProductTakerId { get; set; }
        public DateTime PaymentDate { get; set; }
    }

    public class UpdatePaymentCommandHandler : IRequestHandler<UpdatePaymentCommand>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public UpdatePaymentCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
        {
            Payment? payment = await _context.Payments.FindAsync(request.Id);
            _mapper.Map(request, payment);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
