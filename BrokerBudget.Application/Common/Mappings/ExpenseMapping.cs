using AutoMapper;
using BrokerBudget.Application.UseCases.Expenses;
using BrokerBudget.Application.UseCases.Expenses.Commands.CreateExpense;
using BrokerBudget.Application.UseCases.Expenses.Commands.DeleteExpense;
using BrokerBudget.Application.UseCases.Expenses.Commands.UpdateExpense;
using BrokerBudget.Domain.Entities;

namespace BrokerBudget.Application.Common.Mappings
{
    public class ExpenseMapping : Profile
    {
        public ExpenseMapping()
        {
            CreateMap<CreateExpenseCommand, Expense>().ReverseMap();
            CreateMap<DeleteExpenseCommand, Expense>().ReverseMap();
            CreateMap<UpdateExpenseCommand, Expense>().ReverseMap();
            CreateMap<ExpenseResponse, Expense>().ReverseMap();
        }
    }
}
