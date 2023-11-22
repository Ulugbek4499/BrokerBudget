using System.Data;
using AutoMapper;
using BrokerBudget.Application.Common;
using BrokerBudget.Application.Common.Interfaces;
using ClosedXML.Excel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BrokerBudget.Application.UseCases.Payments.Reports
{
    public class GetPaymentsExcel : IRequest<ExcelReportResponse>
    {
        public string FileName { get; set; }
    }

    public class GetPaymentsExcelHandler : IRequestHandler<GetPaymentsExcel, ExcelReportResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetPaymentsExcelHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ExcelReportResponse> Handle(GetPaymentsExcel request, CancellationToken cancellationToken)
        {
            using (XLWorkbook workbook = new())
            {
                var orderData = await GetPaymentsAsync(cancellationToken);
                var excelSheet = workbook.AddWorksheet(orderData, "Payments");

                excelSheet.RowHeight = 20;
                excelSheet.Column(1).Width = 22;
                excelSheet.Column(2).Width = 22;
                excelSheet.Column(3).Width = 22;
                excelSheet.Column(4).Width = 22;

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream);

                    return new ExcelReportResponse(memoryStream.ToArray(), "Payment/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{request.FileName}.xlsx");
                }
            }
        }

        private async Task<DataTable> GetPaymentsAsync(CancellationToken cancellationToken = default)
        {
            var AllPayments = await _context.Payments.ToListAsync(cancellationToken);

            DataTable excelDataTable = new()
            {
                TableName = "Empdata"
            };

            excelDataTable.Columns.Add("Тўлов Суммаси", typeof(decimal));
            excelDataTable.Columns.Add("Товар Берувчи", typeof(string));
            excelDataTable.Columns.Add("Товар Олувчи", typeof(string));
            excelDataTable.Columns.Add("Тўлов Куни", typeof(DateTime));

            var PaymentsList = _mapper.Map<List<PaymentResponse>>(AllPayments);

            if (PaymentsList.Count > 0)
            {
                PaymentsList.ForEach(item =>
                {
                    excelDataTable.Rows.Add(
                        item.PaymentAmount,
                        item.ProductGiver?.CompanyName,
                        item.ProductTaker?.CompanyName,
                        item.PaymentDate);
                });
            }

            return excelDataTable;
        }
    }
}
