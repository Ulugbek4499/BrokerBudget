using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BrokerBudget.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BrokerBudget.Application.UseCases.Purchases.Queries.GetAllOwnPurchases
{
	public record GetAllOwnPurchasesQuery : IRequest<PurchaseResponse[]>;

	public class GetAllOwnPurchasesQueryHandler : IRequestHandler<GetAllOwnPurchasesQuery, PurchaseResponse[]>
	{
		private readonly IMapper _mapper;
		private readonly IApplicationDbContext _context;

		public GetAllOwnPurchasesQueryHandler(IMapper mapper, IApplicationDbContext context)
		{
			_mapper = mapper;
			_context = context;
		}

		public async Task<PurchaseResponse[]> Handle(GetAllOwnPurchasesQuery request, CancellationToken cancellationToken)
		{
			var purchases = await _context.Purchases.Where(x=>x.ProductTaker==null).ToArrayAsync();

			return _mapper.Map<PurchaseResponse[]>(purchases);
		}
	}
}
