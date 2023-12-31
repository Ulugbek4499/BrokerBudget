﻿using BrokerBudget.Application.UseCases.Reports;
using GameStore.Domain.States;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrokerBudget.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
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
