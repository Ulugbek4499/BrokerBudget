﻿using BrokerBudget.Application.UseCases.ProductGivers.Queries.GetAllProductGivers;
using BrokerBudget.Application.UseCases.ProductGivers;
using BrokerBudget.Application.UseCases.ProductTakers;
using BrokerBudget.Application.UseCases.ProductTakerTakers.Queries.GetAllProductTakerTakers;
using BrokerBudget.Application.UseCases.Purchases.Commands.CreatePurchase;
using BrokerBudget.Application.UseCases.Purchases.Commands.DeletePurchase;
using BrokerBudget.Application.UseCases.Purchases.Commands.UpdatePurchase;
using BrokerBudget.Application.UseCases.Purchases.Queries.GetAllPurchases;
using BrokerBudget.Application.UseCases.Purchases.Queries.GetPurchaseById;
using Microsoft.AspNetCore.Mvc;
using BrokerBudget.Application.UseCases.Products;
using BrokerBudget.Application.UseCases.Products.Queries.GetAllProducts;
using BrokerBudget.Application.UseCases.ProductGivers;
using BrokerBudget.Application.UseCases.Purchases.Reports;

namespace BrokerBudget.MVC.Controllers
{
    public class PurchaseController : ApiBaseController
    {
        [HttpGet("[action]")]
        public async ValueTask<IActionResult> CreatePurchase()
        {
            ProductResponse[] products = await Mediator.Send(new GetAllProductsQuery());
            ViewData["Products"] = products;

            ProductGiverResponse[] productGivers = await Mediator.Send(new GetAllProductGiversQuery());
            ViewData["ProductGivers"] = productGivers;

            ProductTakerResponse[] productTakers = await Mediator.Send(new GetAllProductTakersQuery());
            ViewData["ProductTakers"] = productTakers;

            return View();
        }

        [HttpPost("[action]")]
        public async ValueTask<IActionResult> CreatePurchase([FromForm] CreatePurchaseCommand Purchase)
        {
            await Mediator.Send(Purchase);

            return RedirectToAction("GetAllPurchases");
        }

        [HttpGet("[action]")]
        public async ValueTask<IActionResult> CreatePurchaseFromExcel()
        {
            return View();
        }

        [HttpGet("[action]")]
        public async ValueTask<IActionResult> GetAllPurchases()
        {
            var Purchases = await Mediator.Send(new GetAllPurchasesQuery());

            return View(Purchases);
        }

        [HttpGet("[action]")]
        public async ValueTask<FileResult> GetAllPurchasesExcel(string fileName = "Барча_харидлар")
        {
            var result = await Mediator.Send(new GetPurchasesExcel { FileName = fileName });

            return File(result.FileContents, result.Option, result.FileName);
        }

        [HttpGet("[action]")]
        public async ValueTask<IActionResult> UpdatePurchase(int Id)
        {
            var Purchase = await Mediator.Send(new GetPurchaseByIdQuery(Id));

            ProductResponse[] products = await Mediator.Send(new GetAllProductsQuery());
            ViewData["Products"] = products;

            ProductGiverResponse[] productGivers = await Mediator.Send(new GetAllProductGiversQuery());
            ViewData["ProductGivers"] = productGivers;

            ProductTakerResponse[] productTakers = await Mediator.Send(new GetAllProductTakersQuery());
            ViewData["ProductTakers"] = productTakers;

            return View(Purchase);
        }

        [HttpPost("[action]")]
        public async ValueTask<IActionResult> UpdatePurchase([FromForm] UpdatePurchaseCommand Purchase)
        {
            await Mediator.Send(Purchase);
            return RedirectToAction("GetAllPurchases");
        }

        public async ValueTask<IActionResult> DeletePurchase(int Id)
        {
            await Mediator.Send(new DeletePurchaseCommand(Id));

            return RedirectToAction("GetAllPurchases");
        }

        [HttpGet("[action]")]
        public async ValueTask<IActionResult> ViewPurchase(int id)
        {
            var Purchase = await Mediator.Send(new GetPurchaseByIdQuery(id));

            return View("ViewPurchase", Purchase);
        }
    }
}
