using BrokerBudget.Application.UseCases.Reports;
using Microsoft.AspNetCore.Mvc;

namespace BrokerBudget.MVC.Controllers
{
    public class ReportController : ApiBaseController
    {
        [HttpGet("[action]")]
        public async ValueTask<IActionResult> GetStatistics()
        {
            var statistic = await Mediator.Send(new GetStatisticsQuery());

            return View(statistic);
        }
    }
}
