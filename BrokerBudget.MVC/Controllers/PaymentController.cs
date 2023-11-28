using BrokerBudget.Application.UseCases.ProductGivers;
using BrokerBudget.Application.UseCases.Payments.Commands.CreatePayment;
using BrokerBudget.Application.UseCases.Payments.Commands.DeletePayment;
using BrokerBudget.Application.UseCases.Payments.Commands.UpdatePayment;
using BrokerBudget.Application.UseCases.Payments.Queries.GetPaymentById;
using BrokerBudget.Application.UseCases.ProductGivers.Queries.GetAllProductGivers;
using BrokerBudget.Application.UseCases.ProductTakers;
using BrokerBudget.Application.UseCases.ProductTakerTakers.Queries.GetAllProductTakerTakers;
using Microsoft.AspNetCore.Mvc;
using BrokerBudget.Application.UseCases.Payments.Reports;
using BrokerBudget.Application.UseCases.Payments.Queries.GetAllClientPayments;
using BrokerBudget.Application.UseCases.Payments.Queries.GetAllOwnPayments;

namespace BrokerBudget.MVC.Controllers
{
	public class PaymentController : ApiBaseController
    {
        [HttpGet("[action]")]
        public async ValueTask<IActionResult> CreateOwnPayment()
        {
            ProductGiverResponse[] productGivers = await Mediator.Send(new GetAllProductGiversQuery());
            ViewData["ProductGivers"] = productGivers;

            return View();
        }

        [HttpPost("[action]")]
        public async ValueTask<IActionResult> CreateOwnPayment([FromForm] CreatePaymentCommand Payment)
        {
            await Mediator.Send(Payment);

            return RedirectToAction("GetAllOwnPayments");
        }

        [HttpGet("[action]")]
        public async ValueTask<IActionResult> CreateClientPayment()
        {

            ProductTakerResponse[] productTakers = await Mediator.Send(new GetAllProductTakersQuery());
            ViewData["ProductTakers"] = productTakers;

            return View();
        }

        [HttpPost("[action]")]
        public async ValueTask<IActionResult> CreateClientPayment([FromForm] CreatePaymentCommand Payment)
        {
            await Mediator.Send(Payment);

            return RedirectToAction("GetAllClientPayments");
        }

        [HttpGet("[action]")]
        public async ValueTask<IActionResult> GetAllClientPayments()
        {
            var Payments = await Mediator.Send(new GetAllClientPaymentsQuery());

            return View(Payments);
        }
		[HttpGet("[action]")]
		public async ValueTask<IActionResult> GetAllOwnPayments()
		{
			var Payments = await Mediator.Send(new GetAllOwnPaymentsQuery());

			return View(Payments);
		}

		[HttpGet("[action]")]
        public async ValueTask<FileResult> GetAllPaymentsExcel(string fileName = "Барча_Tўловлар")
        {
            var result = await Mediator.Send(new GetPaymentsExcel { FileName = fileName });

            return File(result.FileContents, result.Option, result.FileName);
        }

        [HttpGet("[action]")]
        public async ValueTask<IActionResult> UpdatePayment(int Id)
        {
            ProductGiverResponse[] productGivers = await Mediator.Send(new GetAllProductGiversQuery());
            ViewData["ProductGivers"] = productGivers;

            ProductTakerResponse[] productTakers = await Mediator.Send(new GetAllProductTakersQuery());
            ViewData["ProductTakers"] = productTakers;

            var Payment = await Mediator.Send(new GetPaymentByIdQuery(Id));

            return View(Payment);
        }

        [HttpPost("[action]")]
        public async ValueTask<IActionResult> UpdatePayment([FromForm] UpdatePaymentCommand Payment)
        {
            await Mediator.Send(Payment);

			if (Payment.ProductGiverId is null)
			{
				return RedirectToAction("GetAllOwnPayments");
			}

			return RedirectToAction("GetAllClientPayments");
		}

        public async ValueTask<IActionResult> DeletePayment(int Id)
        {
            await Mediator.Send(new DeletePaymentCommand(Id));

            return RedirectToAction("GetAllPayments");
        }

        [HttpGet("[action]")]
        public async ValueTask<IActionResult> ViewPayment(int id)
        {
            var Payment = await Mediator.Send(new GetPaymentByIdQuery(id));

            return View("ViewPayment", Payment);
        }
    }
}
