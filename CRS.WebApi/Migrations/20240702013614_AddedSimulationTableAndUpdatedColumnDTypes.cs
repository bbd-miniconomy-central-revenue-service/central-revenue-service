using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRS.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedSimulationTableAndUpdatedColumnDTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaxPayer_TaxPayment",
                table: "TaxPayment");

            migrationBuilder.AlterColumn<long>(
                name: "taxPayerID",
                table: "TaxPayment",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.DropPrimaryKey(name: "PK_TaxPaymentId", table: "TaxPayment");

            migrationBuilder.AlterColumn<long>(
                name: "id",
                table: "TaxPayment",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaxPaymentId",
                table: "TaxPayment",
                column: "id");

            migrationBuilder.AlterColumn<long>(
                name: "personaId",
                table: "TaxPayer",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.DropPrimaryKey(name: "PK_TaxPayerId", table: "TaxPayer");

            migrationBuilder.AlterColumn<long>(
                name: "id",
                table: "TaxPayer",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaxPayerId",
                table: "TaxPayer",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaxPayer_TaxPayment",
                table: "TaxPayment",
                column: "taxPayerID",
                principalTable: "TaxPayer",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddColumn<int>(
                name: "simulationId",
                table: "TaxPayer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Simulation",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    startTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Simulation", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaxPayer_simulationId",
                table: "TaxPayer",
                column: "simulationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaxPayer_Simulation_simulationId",
                table: "TaxPayer",
                column: "simulationId",
                principalTable: "Simulation",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaxPayer_Simulation_simulationId",
                table: "TaxPayer");

            migrationBuilder.DropTable(
                name: "Simulation");

            migrationBuilder.DropIndex(
                name: "IX_TaxPayer_simulationId",
                table: "TaxPayer");

            migrationBuilder.DropColumn(
                name: "simulationId",
                table: "TaxPayer");

            migrationBuilder.AlterColumn<int>(
                name: "taxPayerID",
                table: "TaxPayment",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "TaxPayment",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "personaId",
                table: "TaxPayer",
                type: "int",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "TaxPayer",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}
