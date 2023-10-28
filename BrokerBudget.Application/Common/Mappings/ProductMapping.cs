using AutoMapper;
using BrokerBudget.Application.UseCases.Products;
using BrokerBudget.Application.UseCases.Products.Commands.CreateProduct;
using BrokerBudget.Application.UseCases.Products.Commands.DeleteProduct;
using BrokerBudget.Application.UseCases.Products.Commands.UpdateProduct;
using BrokerBudget.Domain.Entities;

namespace BrokerBudget.Application.Common.Mappings
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<CreateProductCommand, Product>().ReverseMap();
            CreateMap<DeleteProductCommand, Product>().ReverseMap();
            CreateMap<UpdateProductCommand, Product>().ReverseMap();
            CreateMap<ProductResponse, Product>().ReverseMap();

        }
    }
}
