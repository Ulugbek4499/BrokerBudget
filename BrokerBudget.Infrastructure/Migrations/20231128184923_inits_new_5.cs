using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrokerBudget.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class inits_new_5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "FinalPriceOfPurchase",
                table: "Purchases",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<decimal>(
                name: "TakenMoneyAmount",
                table: "Purchases",
                type: "numeric",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TakenMoneyAmount",
                table: "Purchases");

            migrationBuilder.AlterColumn<decimal>(
                name: "FinalPriceOfPurchase",
                table: "Purchases",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);
        }
    }
}
