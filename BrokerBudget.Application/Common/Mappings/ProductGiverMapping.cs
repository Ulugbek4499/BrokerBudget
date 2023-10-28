using AutoMapper;
using BrokerBudget.Application.UseCases.ProductGivers;
using BrokerBudget.Application.UseCases.ProductGivers.Commands.CreateProductGiver;
using BrokerBudget.Application.UseCases.ProductGivers.Commands.DeleteProductGiver;
using BrokerBudget.Application.UseCases.ProductGivers.Commands.UpdateProductGiver;
using BrokerBudget.Domain.Entities;

namespace BrokerBudget.Application.Common.Mappings
{
    public class ProductGiverMapping : Profile
    {
        public ProductGiverMapping()
        {
            CreateMap<CreateProductGiverCommand, ProductGiver>().ReverseMap();
            CreateMap<DeleteProductGiverCommand, ProductGiver>().ReverseMap();
            CreateMap<UpdateProductGiverCommand, ProductGiver>().ReverseMap();
            CreateMap<ProductGiverResponse, ProductGiver>().ReverseMap();

        }
    }
}
