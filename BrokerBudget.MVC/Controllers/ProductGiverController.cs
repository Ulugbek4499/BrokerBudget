﻿using BrokerBudget.Application.UseCases.ProductGivers.Commands.CreateProductGiver;
using BrokerBudget.Application.UseCases.ProductGivers.Commands.DeleteProductGiver;
using BrokerBudget.Application.UseCases.ProductGivers.Commands.UpdateProductGiver;
using BrokerBudget.Application.UseCases.ProductGivers.Queries.GetAllProductGivers;
using BrokerBudget.Application.UseCases.ProductGivers.Queries.GetProductGiverById;
using Microsoft.AspNetCore.Mvc;

namespace BrokerBudget.MVC.Controllers
{
    public class ProductGiverController : ApiBaseController
    {
        [HttpGet("[action]")]
        public async ValueTask<IActionResult> CreateProductGiver()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async ValueTask<IActionResult> CreateProductGiver([FromForm] CreateProductGiverCommand ProductGiver)
        {
            await Mediator.Send(ProductGiver);

            return RedirectToAction("GetAllProductGivers");
        }

        [HttpGet("[action]")]
        public async ValueTask<IActionResult> CreateProductGiverFromExcel()
        {
            return View();
        }

        /*        [HttpPost("[action]")]
                public async ValueTask<IActionResult> CreateProductGiverFromExcel(IFormFile excelfile)
                {
                    var result = await Mediator.Send(new AddProductGiversFromExcel(excelfile));

                    return RedirectToAction("GetAllProductGivers");
                }*/

        [HttpGet("[action]")]
        public async ValueTask<IActionResult> GetAllProductGivers()
        {
            var ProductGivers = await Mediator.Send(new GetAllProductGiversQuery());

            return View(ProductGivers);
        }

        /*        [HttpGet("[action]")]
                public async ValueTask<FileResult> GetAllProductGiversExcel(string fileName = "AllProductGivers")
                {
                    var result = await Mediator.Send(new GetProductGiversExcel { FileName = fileName });

                    return File(result.FileContents, result.Option, result.FileName);
                }*/

        [HttpGet("[action]")]
        public async ValueTask<IActionResult> UpdateProductGiver(int Id)
        {
            var ProductGiver = await Mediator.Send(new GetProductGiverByIdQuery(Id));

            return View(ProductGiver);
        }

        [HttpPost("[action]")]
        public async ValueTask<IActionResult> UpdateProductGiver([FromForm] UpdateProductGiverCommand ProductGiver)
        {
            await Mediator.Send(ProductGiver);
            return RedirectToAction("GetAllProductGivers");
        }

        public async ValueTask<IActionResult> DeleteProductGiver(int Id)
        {
            await Mediator.Send(new DeleteProductGiverCommand(Id));

            return RedirectToAction("GetAllProductGivers");
        }

        [HttpGet("[action]")]
        public async ValueTask<IActionResult> ViewProductGiver(int id)
        {
            var ProductGiver = await Mediator.Send(new GetProductGiverByIdQuery(id));

            return View("ViewProductGiver", ProductGiver);
        }
    }
}