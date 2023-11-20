﻿using BrokerBudget.Application.UseCases.Expenses.Commands.CreateExpense;
using BrokerBudget.Application.UseCases.Expenses.Commands.DeleteExpense;
using BrokerBudget.Application.UseCases.Expenses.Commands.UpdateExpense;
using BrokerBudget.Application.UseCases.Expenses.Queries.GetAllExpenses;
using BrokerBudget.Application.UseCases.Expenses.Queries.GetExpenseById;
using Microsoft.AspNetCore.Mvc;

namespace BrokerBudget.MVC.Controllers
{
    public class ExpenseController : ApiBaseController
    {
        [HttpGet("[action]")]
        public async ValueTask<IActionResult> CreateExpense()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async ValueTask<IActionResult> CreateExpense([FromForm] CreateExpenseCommand Expense)
        {
            await Mediator.Send(Expense);

            return RedirectToAction("GetAllExpenses");
        }

        [HttpGet("[action]")]
        public async ValueTask<IActionResult> CreateExpenseFromExcel()
        {
            return View();
        }

        /*        [HttpPost("[action]")]
                public async ValueTask<IActionResult> CreateExpenseFromExcel(IFormFile excelfile)
                {
                    var result = await Mediator.Send(new AddExpensesFromExcel(excelfile));

                    return RedirectToAction("GetAllExpenses");
                }*/

        [HttpGet("[action]")]
        public async ValueTask<IActionResult> GetAllExpenses()
        {
            var Expenses = await Mediator.Send(new GetAllExpensesQuery());

            return View(Expenses);
        }

        /*        [HttpGet("[action]")]
                public async ValueTask<FileResult> GetAllExpensesExcel(string fileName = "AllExpenses")
                {
                    var result = await Mediator.Send(new GetExpensesExcel { FileName = fileName });

                    return File(result.FileContents, result.Option, result.FileName);
                }*/

        [HttpGet("[action]")]
        public async ValueTask<IActionResult> UpdateExpense(int Id)
        {
            var Expense = await Mediator.Send(new GetExpenseByIdQuery(Id));

            return View(Expense);
        }

        [HttpPost("[action]")]
        public async ValueTask<IActionResult> UpdateExpense([FromForm] UpdateExpenseCommand Expense)
        {
            await Mediator.Send(Expense);
            return RedirectToAction("GetAllExpenses");
        }

        public async ValueTask<IActionResult> DeleteExpense(int Id)
        {
            await Mediator.Send(new DeleteExpenseCommand(Id));

            return RedirectToAction("GetAllExpenses");
        }

        [HttpGet("[action]")]
        public async ValueTask<IActionResult> ViewExpense(int id)
        {
            var Expense = await Mediator.Send(new GetExpenseByIdQuery(id));

            return View("ViewExpense", Expense);
        }
    }
}