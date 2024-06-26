using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRS.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class ReaddPersonaIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "personaId",
                table: "TaxPayer",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "personaId",
                table: "TaxPayer");
        }
    }
}
