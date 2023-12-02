using AutoMapper;
using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Application.Common;
using ClosedXML.Excel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;
using BrokerBudget.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace BrokerBudget.Application.UseCases.Expenses.Queries.Reports
{
    public class GetExpensesExcel : IRequest<ExcelReportResponse>
    {
        public string FileName { get; set; }
    }

    public class GetExpensesExcelHandler : IRequestHandler<GetExpensesExcel, ExcelReportResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public GetExpensesExcelHandler(IApplicationDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ExcelReportResponse> Handle(GetExpensesExcel request, CancellationToken cancellationToken)
        {
            using (XLWorkbook workbook = new())
            {
                var orderData = await GetExpensesAsync(cancellationToken);
                var excelSheet = workbook.AddWorksheet(orderData, "Expenses");

                excelSheet.RowHeight = 20;
                excelSheet.Column(1).Width = 22;
                excelSheet.Column(2).Width = 22;
                excelSheet.Column(3).Width = 40;
                excelSheet.Column(4).Width = 22;
                excelSheet.Column(5).Width = 22;

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream);

                    return new ExcelReportResponse(memoryStream.ToArray(), "Expense/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{request.FileName}.xlsx");
                }
            }
        }

        private async Task<DataTable> GetExpensesAsync(CancellationToken cancellationToken = default)
        {
            var AllExpenses = await _context.Expenses.ToListAsync(cancellationToken);

            DataTable excelDataTable = new()
            {
                TableName = "Empdata"
            };

            excelDataTable.Columns.Add("Харажат Суммаси", typeof(decimal));
            excelDataTable.Columns.Add("Харажат Куни", typeof(DateTime));
            excelDataTable.Columns.Add("Изоҳ", typeof(string));
            excelDataTable.Columns.Add("CreatedBy", typeof(string));
            excelDataTable.Columns.Add("CreatedAt", typeof(DateTime));

            var ExpensesList = _mapper.Map<List<ExpenseResponse>>(AllExpenses);

            if (ExpensesList.Count > 0)
            {
                foreach (var item in ExpensesList)
                {
                    // Fetch user details based on CreatedBy
                    var creator = await _userManager.FindByIdAsync(item.CreatedBy);
                    var createdByFullName = creator != null
                        ? $"{creator.FirstName} {creator.LastName}"
                        : "User Not Found";

                    excelDataTable.Rows.Add(
                        item.Amount,
                        item.ExpenseDate,
                        item.Note,
                        createdByFullName,
                        item.CreatedDate);
                }
            }

            return excelDataTable;
        }


    }
}
