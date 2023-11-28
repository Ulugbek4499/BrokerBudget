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

	public record GetAllCustomerPurchasesQuery : IRequest<PurchaseResponse[]>;

	public class GetAllCustomerPurchasesQueryHandler : IRequestHandler<GetAllCustomerPurchasesQuery, PurchaseResponse[]>
	{
		private readonly IMapper _mapper;
		private readonly IApplicationDbContext _context;

		public GetAllCustomerPurchasesQueryHandler(IMapper mapper, IApplicationDbContext context)
		{
			_mapper = mapper;
			_context = context;
		}

		public async Task<PurchaseResponse[]> Handle(GetAllCustomerPurchasesQuery request, CancellationToken cancellationToken)
		{
			var purchases = await _context.Purchases.Where(x=>x.ProductGiverId==null).ToArrayAsync();

			return _mapper.Map<PurchaseResponse[]>(purchases);
		}
	}
}
