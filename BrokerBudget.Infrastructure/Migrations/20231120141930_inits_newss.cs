using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrokerBudget.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class inits_newss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalPriceOfPurchase",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "finalPriceOfPurchase",
                table: "Purchases");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "FinalPriceOfPurchase",
                table: "Purchases",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "finalPriceOfPurchase",
                table: "Purchases",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
