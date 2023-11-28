using AutoMapper;
using BrokerBudget.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BrokerBudget.Application.UseCases.Payments.Queries.GetAllOwnPayments
{
	public record GetAllOwnPaymentsQuery : IRequest<PaymentResponse[]>;

	public class GetAllOwnPaymentsQueryHandler : IRequestHandler<GetAllOwnPaymentsQuery, PaymentResponse[]>
	{
		private readonly IMapper _mapper;
		private readonly IApplicationDbContext _context;

		public GetAllOwnPaymentsQueryHandler(IMapper mapper, IApplicationDbContext context)
		{
			_mapper = mapper;
			_context = context;
		}

		public async Task<PaymentResponse[]> Handle(GetAllOwnPaymentsQuery request, CancellationToken cancellationToken)
		{
			var Payments = await _context.Payments.Where(x => x.ProductTaker == null).ToArrayAsync();

			return _mapper.Map<PaymentResponse[]>(Payments);
		}
	}
}
