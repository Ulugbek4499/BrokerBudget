using AutoMapper;
using BrokerBudget.Application.UseCases.ProductTakers.Commands.CreateProductTaker;
using BrokerBudget.Application.UseCases.ProductTakers.Commands.DeleteProductTaker;
using BrokerBudget.Application.UseCases.ProductTakers.Commands.UpdateProductTaker;
using BrokerBudget.Application.UseCases.ProductTakers;
using BrokerBudget.Domain.Entities;

namespace BrokerBudget.Application.Common.Mappings
{
    public class ProductTakerMapping : Profile
    {
        public ProductTakerMapping()
        {
            CreateMap<CreateProductTakerCommand, ProductTaker>().ReverseMap();
            CreateMap<DeleteProductTakerCommand, ProductTaker>().ReverseMap();
            CreateMap<UpdateProductTakerCommand, ProductTaker>().ReverseMap();
            CreateMap<ProductTakerResponse, ProductTaker>().ReverseMap();

        }
    }
}
