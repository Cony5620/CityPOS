using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityPOS.Migrations
{
    /// <inheritdoc />
    public partial class AddSaleTypeToSaleOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SaleType",
                table: "SaleOrder",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaleType",
                table: "SaleOrder");
        }
    }
}
