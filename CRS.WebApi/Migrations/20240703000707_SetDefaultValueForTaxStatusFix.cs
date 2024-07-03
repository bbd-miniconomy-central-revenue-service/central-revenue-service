using Microsoft.EntityFrameworkCore.Migrations;
using CRS.WebApi.Data;

#nullable disable

namespace CRS.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class SetDefaultValueForTaxStatusFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "TaxPayer",
                type: "int",
                defaultValue: (int)TaxStatus.INACTIVE);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "TaxPayer",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: (int)TaxStatus.INACTIVE);
        }
    }
}
