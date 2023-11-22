using AutoMapper;
using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Application.Common;
using ClosedXML.Excel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;

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

        public GetExpensesExcelHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

            var ExpensesList = _mapper.Map<List<ExpenseResponse>>(AllExpenses);

            if (ExpensesList.Count > 0)
            {
                ExpensesList.ForEach(item =>
                {
                    excelDataTable.Rows.Add(
                        item.Amount,
                        item.ExpenseDate,
                        item.Note);
                });
            }

            return excelDataTable;
        }
    }
}
