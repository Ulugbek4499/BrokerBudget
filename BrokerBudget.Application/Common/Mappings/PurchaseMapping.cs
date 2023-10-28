using AutoMapper;
using BrokerBudget.Application.UseCases.Purchases;
using BrokerBudget.Application.UseCases.Purchases.Commands.CreatePurchase;
using BrokerBudget.Application.UseCases.Purchases.Commands.DeletePurchase;
using BrokerBudget.Application.UseCases.Purchases.Commands.UpdatePurchase;
using BrokerBudget.Domain.Entities;

namespace BrokerBudget.Application.Common.Mappings
{
    public class PurchaseMapping : Profile
    {
        public PurchaseMapping()
        {
            CreateMap<CreatePurchaseCommand, Purchase>().ReverseMap();
            CreateMap<DeletePurchaseCommand, Purchase>().ReverseMap();
            CreateMap<UpdatePurchaseCommand, Purchase>().ReverseMap();
            CreateMap<PurchaseResponse, Purchase>().ReverseMap();

        }
    }
}
