using BrokerBudget.Application.UseCases.ProductTakers.Commands.CreateProductTaker;
using BrokerBudget.Application.UseCases.ProductTakers.Commands.DeleteProductTaker;
using BrokerBudget.Application.UseCases.ProductTakers.Commands.UpdateProductTaker;
using BrokerBudget.Application.UseCases.ProductTakers.Queries.GetProductTakerTakerById;
using BrokerBudget.Application.UseCases.ProductTakers.Reports;
using BrokerBudget.Application.UseCases.ProductTakerTakers.Queries.GetAllProductTakerTakers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrokerBudget.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductTakerController : ApiBaseController
    {
        [HttpGet("[action]")]
        public async ValueTask<IActionResult> CreateProductTaker()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async ValueTask<IActionResult> CreateProductTaker([FromForm] CreateProductTakerCommand ProductTaker)
        {
            await Mediator.Send(ProductTaker);

            return RedirectToAction("GetAllProductTakers");
        }

        [HttpGet("[action]")]
        public async ValueTask<IActionResult> CreateProductTakerFromExcel()
        {
            return View();
        }

        [HttpGet("[action]")]
        public async ValueTask<IActionResult> GetAllProductTakers()
        {
            var ProductTakers = await Mediator.Send(new GetAllProductTakersQuery());

            return View(ProductTakers);
        }

        [HttpGet("[action]")]
        public async ValueTask<FileResult> GetAllProductTakersExcel(string fileName = "Юк_Олувчилар")
        {
            var result = await Mediator.Send(new GetProductTakersExcel { FileName = fileName });

            return File(result.FileContents, result.Option, result.FileName);
        }

        [HttpGet("[action]")]
        public async ValueTask<IActionResult> UpdateProductTaker(int Id)
        {
            var ProductTaker = await Mediator.Send(new GetProductTakerByIdQuery(Id));

            return View(ProductTaker);
        }

        [HttpPost("[action]")]
        public async ValueTask<IActionResult> UpdateProductTaker([FromForm] UpdateProductTakerCommand ProductTaker)
        {
            await Mediator.Send(ProductTaker);
            return RedirectToAction("GetAllProductTakers");
        }

        public async ValueTask<IActionResult> DeleteProductTaker(int Id)
        {
            await Mediator.Send(new DeleteProductTakerCommand(Id));

            return RedirectToAction("GetAllProductTakers");
        }
    }
}
