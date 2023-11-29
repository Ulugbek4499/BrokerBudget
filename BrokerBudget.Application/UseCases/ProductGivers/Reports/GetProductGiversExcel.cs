using AutoMapper;
using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Application.Common;
using ClosedXML.Excel;
using MediatR;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace BrokerBudget.Application.UseCases.ProductGivers.Reports
{
    public class GetProductGiversExcel : IRequest<ExcelReportResponse>
    {
        public string FileName { get; set; }
    }

    public class GetProductGiversExcelHandler : IRequestHandler<GetProductGiversExcel, ExcelReportResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProductGiversExcelHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ExcelReportResponse> Handle(GetProductGiversExcel request, CancellationToken cancellationToken)
        {
            using (XLWorkbook workbook = new())
            {
                var orderData = await GetProductGiversAsync(cancellationToken);
                var excelSheet = workbook.AddWorksheet(orderData, "ProductGivers");

                excelSheet.RowHeight = 20;
                excelSheet.Column(1).Width = 18;
                excelSheet.Column(2).Width = 18;
                excelSheet.Column(3).Width = 18;
                excelSheet.Column(4).Width = 18;
                excelSheet.Column(5).Width = 18;
                excelSheet.Column(6).Width = 18;
                excelSheet.Column(7).Width = 18;
                excelSheet.Column(8).Width = 18;

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream);

                    return new ExcelReportResponse(memoryStream.ToArray(), "Purchase/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{request.FileName}.xlsx");
                }
            }
        }

        private async Task<DataTable> GetProductGiversAsync(CancellationToken cancellationToken = default)
        {
            var AllProductGivers = await _context.ProductGivers.ToListAsync(cancellationToken);

            DataTable excelDataTable = new()
            {
                TableName = "Empdata"
            };

            excelDataTable.Columns.Add("Корхона номи", typeof(string));

            excelDataTable.Columns.Add("Юк Олдим", typeof(decimal));
            excelDataTable.Columns.Add("Пул Бердим", typeof(decimal));
            excelDataTable.Columns.Add("Oстаток", typeof(decimal));

            excelDataTable.Columns.Add("Маъсул шахс", typeof(string));
            excelDataTable.Columns.Add("Телефон рақам", typeof(string));
            excelDataTable.Columns.Add("INN", typeof(string));
            excelDataTable.Columns.Add("Банк хисоб рақам", typeof(string));

            var ProductGiversList = _mapper.Map<List<ProductGiverResponse>>(AllProductGivers);

            if (ProductGiversList.Count > 0)
            {
                ProductGiversList.ForEach(item =>
                {
                    decimal? totalPurchase = item.Purchases?.Sum(p => p.FinalPriceOfPurchase);
                    decimal? totalPayment = item.Payments?.Sum(p => p.PaymentAmount);
                    decimal? debtAmount = totalPurchase - totalPayment;

                    excelDataTable.Rows.Add(
                        item.CompanyName,
                        totalPurchase,
                        totalPayment,
                        debtAmount,
                        item.ResponsiblePersonName,
                        item.PhoneNumber,
                        item.INN,
                        item.BankAccountNumber);
                });
            }

            return excelDataTable;
        }
    }
}
