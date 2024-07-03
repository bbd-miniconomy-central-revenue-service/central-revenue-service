using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRS.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class CreateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "TaxPayerType",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxPayerTypeId", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TaxStatus",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxStatusId", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TaxType",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())"),
                    rate = table.Column<decimal>(type: "decimal(5,5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxTypeId", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    created = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserId", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TaxPayer",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    personaId = table.Column<long>(type: "bigint", nullable: true),
                    taxPayerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    group = table.Column<int>(type: "int", nullable: false),
                    simulationId = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())"),
                    updated = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())"),
                    amountOwing = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxPayerId", x => x.id);
                    table.ForeignKey(
                        name: "FK_TaxPayer_Simulation_simulationId",
                        column: x => x.simulationId,
                        principalTable: "Simulation",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaxPayer_TaxPyerType",
                        column: x => x.group,
                        principalTable: "TaxPayerType",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_TaxPayer_TaxStatus",
                        column: x => x.status,
                        principalTable: "TaxStatus",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "TaxPayment",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    taxPayerID = table.Column<long>(type: "bigint", nullable: false),
                    amount = table.Column<decimal>(type: "money", nullable: false),
                    taxType = table.Column<int>(type: "int", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxPaymentId", x => x.id);
                    table.ForeignKey(
                        name: "FK_TaxPayer_TaxPayment",
                        column: x => x.taxPayerID,
                        principalTable: "TaxPayer",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_TaxPayment_TaxType",
                        column: x => x.taxType,
                        principalTable: "TaxType",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaxPayer_group",
                table: "TaxPayer",
                column: "group");

            migrationBuilder.CreateIndex(
                name: "IX_TaxPayer_simulationId",
                table: "TaxPayer",
                column: "simulationId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxPayer_status",
                table: "TaxPayer",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_TaxPayment_taxPayerID",
                table: "TaxPayment",
                column: "taxPayerID");

            migrationBuilder.CreateIndex(
                name: "IX_TaxPayment_taxType",
                table: "TaxPayment",
                column: "taxType");

            migrationBuilder.CreateIndex(
                name: "UQ__User__AB6E6164031D2F51",
                table: "User",
                column: "email",
                unique: true,
                filter: "[email] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaxPayment");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "TaxPayer");

            migrationBuilder.DropTable(
                name: "TaxType");

            migrationBuilder.DropTable(
                name: "Simulation");

            migrationBuilder.DropTable(
                name: "TaxPayerType");

            migrationBuilder.DropTable(
                name: "TaxStatus");
        }
    }
}
