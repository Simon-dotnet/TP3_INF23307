using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Nordik_Aventure.Migrations
{
    /// <inheritdoc />
    public partial class addproductstock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_Sku",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

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
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Sku",
                table: "Products",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Stock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TotalProducts = table.Column<int>(type: "int", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProductInStock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    QuantityInStock = table.Column<int>(type: "int", nullable: false),
                    MinimalQuantity = table.Column<int>(type: "int", nullable: false),
                    StorageLocation = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Threshold = table.Column<int>(type: "int", nullable: false),
                    LastRefill = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    StockId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInStock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductInStock_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductInStock_Stock_StockId",
                        column: x => x.StockId,
                        principalTable: "Stock",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInStock_ProductId",
                table: "ProductInStock",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInStock_StockId",
                table: "ProductInStock",
                column: "StockId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductInStock");

            migrationBuilder.DropTable(
                name: "Stock");

            migrationBuilder.AlterColumn<string>(
                name: "Sku",
                table: "Products",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "GrossMargin", "Name", "PaybackToSupplier", "PriceToBuy", "PriceToSell", "Sku", "Status", "SupplierId", "Weight" },
                values: new object[,]
                {
                    { 1, 1, 51.5, "Tente légère 2 places", 0.050000000000000003, 145.0, 299.0, "NC-TNT-001", "Actif", 1, 2.7999999999999998 },
                    { 2, 1, 47.899999999999999, "Tente familiale 6 places", 0.050000000000000003, 260.0, 499.0, "NC-TNT-002", "Actif", 1, 6.5 },
                    { 3, 1, 57.600000000000001, "Toile imperméable 3x3 m", 0.040000000000000001, 25.0, 59.0, "NC-TNT-003", "Actif", 2, 1.1000000000000001 },
                    { 4, 1, 53.799999999999997, "Tapis de sol isolant", 0.029999999999999999, 18.0, 39.0, "NC-TNT-004", "Actif", 3, 0.90000000000000002 },
                    { 5, 1, 49.700000000000003, "Abri cuisine pliable", 0.050000000000000003, 75.0, 149.0, "NC-TNT-005", "Actif", 1, 5.0 },
                    { 6, 1, 58.600000000000001, "Mat telescopique alu", 0.040000000000000001, 12.0, 29.0, "NC-TNT-006", "Actif", 2, 0.69999999999999996 },
                    { 7, 2, 53.200000000000003, "Sac à dos 50 L etanche", 0.059999999999999998, 65.0, 139.0, "NC-SAC-001", "Actif", 4, 1.3 },
                    { 8, 2, 59.5, "Sac de jour 25 L", 0.059999999999999998, 32.0, 79.0, "NC-SAC-002", "Actif", 4, 0.90000000000000002 },
                    { 9, 2, 52.700000000000003, "Sac de couchage -10 degree", 0.029999999999999999, 80.0, 169.0, "NC-SAC-003", "Actif", 3, 2.2000000000000002 },
                    { 10, 2, 57.600000000000001, "Tapis autogonflant", 0.029999999999999999, 25.0, 59.0, "NC-SAC-004", "Actif", 3, 1.1000000000000001 },
                    { 11, 2, 52.600000000000001, "Housse impermeable sac a dos", 0.040000000000000001, 9.0, 19.0, "NC-SAC-005", "Actif", 2, 0.40000000000000002 },
                    { 12, 2, 55.700000000000003, "Batons de marche carbone", 0.040000000000000001, 35.0, 79.0, "NC-SAC-006", "Actif", 2, 0.80000000000000004 },
                    { 13, 3, 62.700000000000003, "Chandail thermique homme", 0.050000000000000003, 22.0, 59.0, "NC-VET-001", "Actif", 5, 0.59999999999999998 },
                    { 14, 3, 62.700000000000003, "Chandail thermique femme", 0.050000000000000003, 22.0, 59.0, "NC-VET-002", "Actif", 5, 0.59999999999999998 },
                    { 15, 3, 57.299999999999997, "Pantalon de randonnee homme", 0.050000000000000003, 38.0, 89.0, "NC-VET-003", "Actif", 5, 0.80000000000000004 },
                    { 16, 3, 57.299999999999997, "Pantalon de randonnee femme", 0.050000000000000003, 38.0, 89.0, "NC-VET-004", "Actif", 5, 0.80000000000000004 },
                    { 17, 3, 57.399999999999999, "Manteau coupe-vent", 0.040000000000000001, 55.0, 129.0, "NC-VET-005", "Actif", 6, 1.1000000000000001 },
                    { 18, 3, 65.5, "Tuque en laine merinos", 0.040000000000000001, 10.0, 29.0, "NC-VET-006", "Actif", 6, 0.29999999999999999 },
                    { 19, 3, 60.0, "Gants isolants Hiver+", 0.040000000000000001, 18.0, 45.0, "NC-VET-007", "Actif", 6, 0.5 },
                    { 20, 4, 57.600000000000001, "Rechaud portatif", 0.040000000000000001, 25.0, 59.0, "NC-ACC-001", "Actif", 2, 0.90000000000000002 },
                    { 21, 4, 58.600000000000001, "Bouteille isotherme 1L", 0.029999999999999999, 12.0, 29.0, "NC-ACC-002", "Actif", 3, 0.40000000000000002 },
                    { 22, 4, 64.099999999999994, "Lampe frontale 300 lumens", 0.050000000000000003, 14.0, 39.0, "NC-ACC-003", "Actif", 1, 0.20000000000000001 },
                    { 23, 4, 59.200000000000003, "Ensemble vaisselle 4 pers.", 0.040000000000000001, 20.0, 49.0, "NC-ACC-004", "Actif", 2, 1.2 },
                    { 24, 4, 59.399999999999999, "Filtre a eau compact", 0.050000000000000003, 28.0, 69.0, "NC-ACC-005", "Actif", 1, 0.69999999999999996 },
                    { 25, 4, 61.5, "Couteau multifonction", 0.040000000000000001, 15.0, 39.0, "NC-ACC-006", "Actif", 4, 0.5 },
                    { 26, 5, 56.899999999999999, "Montre GPS plein air", 0.040000000000000001, 120.0, 279.0, "NC-ELE-001", "Actif", 7, 0.90000000000000002 },
                    { 27, 5, 55.700000000000003, "Chargeur solaire 20W", 0.040000000000000001, 35.0, 79.0, "NC-ELE-002", "Actif", 7, 0.59999999999999998 },
                    { 28, 5, 62.5, "Boussole de précision", 0.040000000000000001, 9.0, 24.0, "NC-ELE-003", "Actif", 2, 0.20000000000000001 },
                    { 29, 5, 55.100000000000001, "Radio météo portable", 0.040000000000000001, 22.0, 49.0, "NC-ELE-004", "Actif", 7, 0.80000000000000004 },
                    { 30, 5, 56.0, "Lampe USB rechargeable", 0.040000000000000001, 11.0, 25.0, "NC-ELE-005", "Actif", 7, 0.29999999999999999 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_Sku",
                table: "Products",
                column: "Sku",
                unique: true);
        }
    }
}
