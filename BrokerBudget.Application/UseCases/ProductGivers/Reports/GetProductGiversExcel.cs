using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Application.Common;
using BrokerBudget.Application.UseCases.ProductGivers;
using ClosedXML.Excel;
using MediatR;

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

            excelDataTable.Columns.Add("Товаp", typeof(string));
            excelDataTable.Columns.Add("Товар Берувчи", typeof(string));
            excelDataTable.Columns.Add("Товар Олувчи", typeof(string));
            excelDataTable.Columns.Add("Харид куни", typeof(DateTime));
            excelDataTable.Columns.Add("Umumiy Суммаси", typeof(decimal));
            excelDataTable.Columns.Add("Миқдор (Кг/Дона)", typeof(decimal));
            excelDataTable.Columns.Add("Чегирма (Кг/Дона)", typeof(decimal));
            excelDataTable.Columns.Add("Бир дона/кг учун нархи", typeof(decimal));
            excelDataTable.Columns.Add("Умумий суммадан чегирма", typeof(decimal));

            var ProductGiversList = _mapper.Map<List<PurchaseResponse>>(AllProductGivers);

            if (ProductGiversList.Count > 0)
            {
                ProductGiversList.ForEach(item =>
                {
                    excelDataTable.Rows.Add(
                        item.Product.Name,
                        item.ProductGiver?.CompanyName,
                        item.ProductTaker?.CompanyName,
                        item.PurchaseDate,
                        item.FinalPriceOfPurchase,
                        item.Amount,
                        item.SaleAmountCategoryPercentage,
                        item.PricePerAmount,
                        item.SaleForTotalPrice);
                });
            }

            return excelDataTable;
        }
    }
}
