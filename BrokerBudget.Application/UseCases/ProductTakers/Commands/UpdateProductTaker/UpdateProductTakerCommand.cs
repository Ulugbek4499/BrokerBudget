using AutoMapper;
using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Domain.Entities;
using MediatR;

namespace BrokerBudget.Application.UseCases.ProductTakers.Commands.UpdateProductTaker
{
    public class UpdateProductTakerCommand : IRequest
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string ResponsiblePersonName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? INN { get; set; }
        public string? BankAccountNumber { get; set; }
    }

    public class UpdateProductTakerCommandHandler : IRequestHandler<UpdateProductTakerCommand>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public UpdateProductTakerCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task Handle(UpdateProductTakerCommand request, CancellationToken cancellationToken)
        {
            ProductTaker? productTaker = await _context.ProductTakers.FindAsync(request.Id);
            _mapper.Map(request, productTaker);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
