using AutoMapper;
using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Application.Common;
using ClosedXML.Excel;
using MediatR;
using System.Data;
using Microsoft.EntityFrameworkCore;
using BrokerBudget.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace BrokerBudget.Application.UseCases.Purchases.Reports
{
    public class GetPurchasesExcel : IRequest<ExcelReportResponse>
    {
        public string FileName { get; set; }
    }

    public class GetPurchasesExcelHandler : IRequestHandler<GetPurchasesExcel, ExcelReportResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetPurchasesExcelHandler(IApplicationDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ExcelReportResponse> Handle(GetPurchasesExcel request, CancellationToken cancellationToken)
        {
            using (XLWorkbook workbook = new())
            {
                var orderData = await GetPurchasesAsync(cancellationToken);
                var excelSheet = workbook.AddWorksheet(orderData, "Purchases");

                excelSheet.RowHeight = 20;
                excelSheet.Column(1).Width = 18;
                excelSheet.Column(2).Width = 18;
                excelSheet.Column(3).Width = 18;
                excelSheet.Column(4).Width = 18;
                excelSheet.Column(5).Width = 18;
                excelSheet.Column(6).Width = 18;
                excelSheet.Column(7).Width = 18;
                excelSheet.Column(8).Width = 18;
                excelSheet.Column(9).Width = 18;
                excelSheet.Column(10).Width = 18;
                excelSheet.Column(11).Width = 18;

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream);

                    return new ExcelReportResponse(memoryStream.ToArray(), "Purchase/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{request.FileName}.xlsx");
                }
            }
        }

        private async Task<DataTable> GetPurchasesAsync(CancellationToken cancellationToken = default)
        {
            var AllPurchases = await _context.Purchases.ToListAsync(cancellationToken);

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
            excelDataTable.Columns.Add("Ким Томонидан", typeof(string));
            excelDataTable.Columns.Add("Қўшилган кун", typeof(DateTime));

            var PurchasesList = _mapper.Map<List<PurchaseResponse>>(AllPurchases);

            if (PurchasesList.Count > 0)
            {
                foreach (var item in PurchasesList)
                {
                    var creator = await _userManager.FindByIdAsync(item.CreatedBy);
                    var createdByFullName = creator != null
                        ? $"{creator.FirstName} {creator.LastName}"
                        : "User Not Found";

                    excelDataTable.Rows.Add(
                        item.Product.Name,
                        item.ProductGiver?.CompanyName,
                        item.ProductTaker?.CompanyName,
                        item.PurchaseDate,
                        item.FinalPriceOfPurchase,
                        item.Amount,
                        item.SaleAmountCategoryPercentage,
                        item.PricePerAmount,
                        item.SaleForTotalPrice,
                        createdByFullName,
                        item.CreatedDate);
                }
            }

            return excelDataTable;
        }
    }
}
