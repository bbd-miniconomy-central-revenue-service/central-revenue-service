using System;
using CRS.WebApi.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using CRS.WebApi.Data;

#nullable disable

namespace CRS.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TaxType",
                columns: ["id", "description", "rate"],
                values: [((int)Data.TaxType.VAT), Data.TaxType.VAT.ToString(), 0.15]
                );

            migrationBuilder.InsertData(
                table: "TaxType",
                columns: ["id", "description", "rate"],
                values: [(int)Data.TaxType.INCOME, Data.TaxType.INCOME.ToString(), 0.15]
            );

            migrationBuilder.InsertData(
                table: "TaxType",
                columns: ["id", "description", "rate"],
                values: [(int)Data.TaxType.PROPERTY, Data.TaxType.PROPERTY.ToString(), 0.15]
            );

            migrationBuilder.InsertData(
                table: "TaxStatus",
                columns: ["id", "description"],
                values: [(int)Data.TaxStatus.ACTIVE, Data.TaxStatus.ACTIVE.ToString()]
            );

            migrationBuilder.InsertData(
                table: "TaxStatus",
                columns: ["id", "description"],
                values: [(int)Data.TaxStatus.INACTIVE, Data.TaxStatus.INACTIVE.ToString()]
            );

            migrationBuilder.InsertData(
                table: "TaxPayerType",
                columns: ["id", "description"],
                values: [(int)Data.TaxPayerType.INDIVIDUAL, Data.TaxPayerType.INDIVIDUAL.ToString()]
            );

            migrationBuilder.InsertData(
                table: "TaxPayerType",
                columns: ["id", "description"],
                values: [(int)Data.TaxPayerType.BUSINESS, Data.TaxPayerType.BUSINESS.ToString()]
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) { }
    }
}
