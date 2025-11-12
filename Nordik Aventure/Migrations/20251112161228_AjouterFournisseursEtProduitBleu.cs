using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Nordik_Aventure.Migrations
{
    /// <inheritdoc />
    public partial class AjouterFournisseursEtProduitBleu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "HireDate",
                value: new DateTime(2025, 11, 12, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "HireDate",
                value: new DateTime(2025, 11, 12, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3,
                column: "HireDate",
                value: new DateTime(2025, 11, 12, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "PaybackToSupplier", "PriceToBuy", "PriceToSell", "Sku", "Status", "SupplierId", "Weight" },
                values: new object[,]
                {
                    { 2, "Tente familiale 6 places", 0.050000000000000003, 260.0, 499.0, "NC-TNT-002", "Actif", 1, 6.5 },
                    { 5, "Abri cuisine pliable", 0.050000000000000003, 75.0, 149.0, "NC-TNT-005", "Actif", 1, 5.0 }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "AverageDeliveryTime", "Code", "Discount", "Name" },
                values: new object[,]
                {
                    { 2, "5 jour", "TS", 0, "TrekSupply" },
                    { 3, "6 jour", "MN", 0, "MontNord" },
                    { 4, "3 jour", "NP", 0, "NordPack" },
                    { 5, "4 jour", "NW", 0, "NordWear" },
                    { 6, "2 jour", "AL", 0, "ArcticLine" },
                    { 7, "2 jour", "TT", 0, "TechTrail" }
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 1, 5 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "PaybackToSupplier", "PriceToBuy", "PriceToSell", "Sku", "Status", "SupplierId", "Weight" },
                values: new object[,]
                {
                    { 3, "Toile imperméable 3x3 m", 0.040000000000000001, 25.0, 59.0, "NC-TNT-003", "Actif", 2, 1.1000000000000001 },
                    { 4, "Tapis de sol isolant", 0.029999999999999999, 18.0, 39.0, "NC-TNT-004", "Actif", 3, 0.90000000000000002 },
                    { 6, "Mat telescopique alu", 0.040000000000000001, 12.0, 29.0, "NC-TNT-006", "Actif", 2, 0.69999999999999996 }
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { 1, 3 },
                    { 1, 4 },
                    { 1, 6 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 6 });

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "HireDate",
                value: new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "HireDate",
                value: new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3,
                column: "HireDate",
                value: new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
