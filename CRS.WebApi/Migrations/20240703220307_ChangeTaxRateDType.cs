using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRS.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTaxRateDType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "rate",
                table: "TaxType",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "rate",
                table: "TaxType",
                type: "decimal(5,5)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
