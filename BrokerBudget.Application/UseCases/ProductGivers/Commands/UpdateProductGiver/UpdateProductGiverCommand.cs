﻿using AutoMapper;
using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Domain.Entities;
using MediatR;

namespace BrokerBudget.Application.UseCases.ProductGivers.Commands.UpdateProductGiver
{
    public class UpdateProductGiverCommand : IRequest
    {
        public int Id { get; set; }
        public int Count { get; set; } = 1;
        public int CardId { get; set; }
        public int GameId { get; set; }
        public string UserId { get; set; }
    }

    public class UpdateProductGiverCommandHandler : IRequestHandler<UpdateProductGiverCommand>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public UpdateProductGiverCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task Handle(UpdateProductGiverCommand request, CancellationToken cancellationToken)
        {
            ProductGiver? productGiver = await _context.ProductGivers.FindAsync(request.Id);
            _mapper.Map(request, productGiver);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
