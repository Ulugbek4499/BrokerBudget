﻿using BrokerBudget.Application.Common.Exceptions;
using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Domain.Entities;
using MediatR;

namespace BrokerBudget.Application.UseCases.ProductGivers.Commands.DeleteProductGiver
{
    public record DeleteExpenseCommand(int Id) : IRequest;
    public class DeleteProductGiverCommandHandler : IRequestHandler<DeleteExpenseCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteProductGiverCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
        {
            ProductGiver? productGiver = await _context.ProductGivers.FindAsync(request.Id, cancellationToken);

            if (productGiver is null)
                throw new NotFoundException(nameof(productGiver), request.Id);

            _context.ProductGivers.Remove(productGiver);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
