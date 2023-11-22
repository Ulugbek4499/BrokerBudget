using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrokerBudget.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class inits_new55 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "FinalPriceOfPurchase",
                table: "Purchases",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalPriceOfPurchase",
                table: "Purchases");
        }
    }
}
