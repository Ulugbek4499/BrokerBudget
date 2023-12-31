﻿using BrokerBudget.Application.UseCases.ProductGivers.Queries.GetAllProductGivers;
using BrokerBudget.Application.UseCases.ProductGivers;
using BrokerBudget.Application.UseCases.ProductTakers;
using BrokerBudget.Application.UseCases.ProductTakerTakers.Queries.GetAllProductTakerTakers;
using BrokerBudget.Application.UseCases.Purchases.Commands.CreatePurchase;
using BrokerBudget.Application.UseCases.Purchases.Commands.DeletePurchase;
using BrokerBudget.Application.UseCases.Purchases.Commands.UpdatePurchase;
using BrokerBudget.Application.UseCases.Purchases.Queries.GetPurchaseById;
using Microsoft.AspNetCore.Mvc;
using BrokerBudget.Application.UseCases.Products;
using BrokerBudget.Application.UseCases.Products.Queries.GetAllProducts;
using BrokerBudget.Application.UseCases.Purchases.Reports;
using BrokerBudget.Application.UseCases.Purchases.Queries.GetAllOwnPurchases;
using BrokerBudget.Application.UseCases.Purchases.Queries.GetAllCustomerPurchases;
using BrokerBudget.Application.UseCases.Payments.Commands.CreatePayment;
using GameStore.Domain.States;
using Microsoft.AspNetCore.Authorization;

namespace BrokerBudget.MVC.Controllers
{
	public class PurchaseController : ApiBaseController
    {
        [HttpGet("[action]")]
        public async ValueTask<IActionResult> CreateOwnPurchase()
        {
            ProductResponse[] products = await Mediator.Send(new GetAllProductsQuery());
            ViewData["Products"] = products;

            ProductGiverResponse[] productGivers = await Mediator.Send(new GetAllProductGiversQuery());
            ViewData["ProductGivers"] = productGivers;

            return View();
        }

        [HttpPost("[action]")]
        public async ValueTask<IActionResult> CreateOwnPurchase([FromForm] CreatePurchaseCommand Purchase)
        {
            await Mediator.Send(Purchase);

            if (Purchase.TakenMoneyAmount>0)
            {
                 CreatePaymentCommand createPaymentCommand = new CreatePaymentCommand()
                 {
                     PaymentAmount = Purchase.TakenMoneyAmount??0,
                     PaymentDate = Purchase.PurchaseDate,
                     ProductGiverId = Purchase.ProductGiverId,
                 };

                await Mediator.Send(createPaymentCommand);
            }

	        return RedirectToAction("GetAllOwnPurchases");
        }

		[HttpGet("[action]")]
		public async ValueTask<IActionResult> CreateClientPurchase()
		{
			ProductResponse[] products = await Mediator.Send(new GetAllProductsQuery());
			ViewData["Products"] = products;

			ProductTakerResponse[] productTakers = await Mediator.Send(new GetAllProductTakersQuery());
			ViewData["ProductTakers"] = productTakers;

			return View();
		}

		[HttpPost("[action]")]
		public async ValueTask<IActionResult> CreateClientPurchase([FromForm] CreatePurchaseCommand Purchase)
		{
			await Mediator.Send(Purchase);

            if (Purchase.TakenMoneyAmount > 0)
            {
                CreatePaymentCommand createPaymentCommand = new CreatePaymentCommand()
                {
                    PaymentAmount = Purchase.TakenMoneyAmount ?? 0,
                    PaymentDate = Purchase.PurchaseDate,
                    ProductTakerId = Purchase.ProductTakerId
                };

                await Mediator.Send(createPaymentCommand);
            }

            return RedirectToAction("GetAllClientPurchases");
		}

		[HttpGet("[action]")]
        public async ValueTask<IActionResult> CreatePurchaseFromExcel()
        {
            return View();
        }

		[HttpGet("[action]")]
		public async ValueTask<IActionResult> GetAllClientPurchases()
		{
			var Payments = await Mediator.Send(new GetAllClientPurchasesQuery());

			return View(Payments);
		}

		[HttpGet("[action]")]
		public async ValueTask<IActionResult> GetAllOwnPurchases()
		{
			var Payments = await Mediator.Send(new GetAllOwnPurchasesQuery());

			return View(Payments);
		}

        [HttpGet("[action]")]
        [Authorize(Roles = "Admin")]
        public async ValueTask<FileResult> GetAllPurchasesExcel(string fileName = "Барча_Xаридлар")
        {
            var result = await Mediator.Send(new GetPurchasesExcel { FileName = fileName });

            return File(result.FileContents, result.Option, result.FileName);
        }

        [HttpGet("[action]")]
        public async ValueTask<IActionResult> UpdateClientPurchase(int Id)
        {
            var Purchase = await Mediator.Send(new GetPurchaseByIdQuery(Id));

            ProductResponse[] products = await Mediator.Send(new GetAllProductsQuery());
            ViewData["Products"] = products;

            ProductTakerResponse[] productTakers = await Mediator.Send(new GetAllProductTakersQuery());
            ViewData["ProductTakers"] = productTakers;

            return View(Purchase);
        }

        [HttpPost("[action]")]
        public async ValueTask<IActionResult> UpdateClientPurchase([FromForm] UpdatePurchaseCommand Purchase)
        {
            await Mediator.Send(Purchase);

            return RedirectToAction("GetAllClientPurchases");
        }

        [HttpGet("[action]")]
        public async ValueTask<IActionResult> UpdateOwnPurchase(int Id)
        {
            var Purchase = await Mediator.Send(new GetPurchaseByIdQuery(Id));

            ProductResponse[] products = await Mediator.Send(new GetAllProductsQuery());
            ViewData["Products"] = products;

            ProductGiverResponse[] productGivers = await Mediator.Send(new GetAllProductGiversQuery());
            ViewData["ProductGivers"] = productGivers;

            return View(Purchase);
        }

        [HttpPost("[action]")]
        public async ValueTask<IActionResult> UpdateOwnPurchase([FromForm] UpdatePurchaseCommand Purchase)
        {
            await Mediator.Send(Purchase);

    		return RedirectToAction("GetAllOwnPurchases");
		}

        [Authorize(Roles = "Admin")]
        public async ValueTask<IActionResult> DeletePurchase(int Id)
        {
            var purchase = await Mediator.Send(new GetPurchaseByIdQuery(Id));

            await Mediator.Send(new DeletePurchaseCommand(Id));

            if (purchase.ProductTaker is not null)
            {
                return RedirectToAction("GetAllClientPurchases");
            }

            return RedirectToAction("GetAllOwnPurchases");
        }
    }
}
