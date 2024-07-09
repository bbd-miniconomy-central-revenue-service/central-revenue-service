using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRS.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddSettledColumnToPaymentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "settled",
                table: "TaxPayment",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "settled",
                table: "TaxPayment");
        }
    }
}
