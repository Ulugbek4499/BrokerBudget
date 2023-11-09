using BrokerBudget.Application.UseCases.Products.Commands.CreateProduct;
using BrokerBudget.Application.UseCases.Products.Commands.DeleteProduct;
using BrokerBudget.Application.UseCases.Products.Commands.UpdateProduct;
using BrokerBudget.Application.UseCases.Products.Queries.GetAllProducts;
using BrokerBudget.Application.UseCases.Products.Queries.GetProductById;
using Microsoft.AspNetCore.Mvc;

namespace BrokerBudget.MVC.Controllers
{
    public class ProductController : ApiBaseController
    {
        [HttpGet("[action]")]
        public async ValueTask<IActionResult> CreateProduct()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async ValueTask<IActionResult> CreateProduct([FromForm] CreateProductCommand Product)
        {
            await Mediator.Send(Product);

            return RedirectToAction("GetAllProducts");
        }

        [HttpGet("[action]")]
        public async ValueTask<IActionResult> CreateProductFromExcel()
        {
            return View();
        }

        /*        [HttpPost("[action]")]
                public async ValueTask<IActionResult> CreateProductFromExcel(IFormFile excelfile)
                {
                    var result = await Mediator.Send(new AddProductsFromExcel(excelfile));

                    return RedirectToAction("GetAllProducts");
                }*/

        [HttpGet("[action]")]
        public async ValueTask<IActionResult> GetAllProducts()
        {
            var Products = await Mediator.Send(new GetAllProductsQuery());

            return View(Products);
        }

        /*        [HttpGet("[action]")]
                public async ValueTask<FileResult> GetAllProductsExcel(string fileName = "AllProducts")
                {
                    var result = await Mediator.Send(new GetProductsExcel { FileName = fileName });

                    return File(result.FileContents, result.Option, result.FileName);
                }*/

        [HttpGet("[action]")]
        public async ValueTask<IActionResult> UpdateProduct(int Id)
        {
            var Product = await Mediator.Send(new GetProductByIdQuery(Id));

            return View(Product);
        }

        [HttpPost("[action]")]
        public async ValueTask<IActionResult> UpdateProduct([FromForm] UpdateProductCommand Product)
        {
            await Mediator.Send(Product);
            return RedirectToAction("GetAllProducts");
        }

        public async ValueTask<IActionResult> DeleteProduct(int Id)
        {
            await Mediator.Send(new DeleteProductCommand(Id));

            return RedirectToAction("GetAllProducts");
        }

        [HttpGet("[action]")]
        public async ValueTask<IActionResult> ViewProduct(int id)
        {
            var Product = await Mediator.Send(new GetProductByIdQuery(id));

            return View("ViewProduct", Product);
        }
    }
}
