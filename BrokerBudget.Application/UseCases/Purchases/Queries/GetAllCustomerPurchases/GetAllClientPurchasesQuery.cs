using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BrokerBudget.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BrokerBudget.Application.UseCases.Purchases.Queries.GetAllCustomerPurchases
{

	public record GetAllClientPurchasesQuery : IRequest<PurchaseResponse[]>;

	public class GetAllCustomerPurchasesQueryHandler : IRequestHandler<GetAllClientPurchasesQuery, PurchaseResponse[]>
	{
		private readonly IMapper _mapper;
		private readonly IApplicationDbContext _context;

		public GetAllCustomerPurchasesQueryHandler(IMapper mapper, IApplicationDbContext context)
		{
			_mapper = mapper;
			_context = context;
		}

		public async Task<PurchaseResponse[]> Handle(GetAllClientPurchasesQuery request, CancellationToken cancellationToken)
		{
			var purchases = await _context.Purchases.Where(x=>x.ProductGiver==null).ToArrayAsync();

			return _mapper.Map<PurchaseResponse[]>(purchases);
		}
	}
}
