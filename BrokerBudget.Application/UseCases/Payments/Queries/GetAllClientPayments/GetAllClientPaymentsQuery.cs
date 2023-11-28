using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BrokerBudget.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BrokerBudget.Application.UseCases.Payments.Queries.GetAllClientPayments
{
	public record GetAllClientPaymentsQuery : IRequest<PaymentResponse[]>;

	public class GetAllClientPaymentsQueryHandler : IRequestHandler<GetAllClientPaymentsQuery, PaymentResponse[]>
	{
		private readonly IMapper _mapper;
		private readonly IApplicationDbContext _context;

		public GetAllClientPaymentsQueryHandler(IMapper mapper, IApplicationDbContext context)
		{
			_mapper = mapper;
			_context = context;
		}

		public async Task<PaymentResponse[]> Handle(GetAllClientPaymentsQuery request, CancellationToken cancellationToken)
		{
			var Payments = await _context.Payments.Where(x=>x.ProductGiverId==null).ToArrayAsync();

			return _mapper.Map<PaymentResponse[]>(Payments);
		}
	}
}
