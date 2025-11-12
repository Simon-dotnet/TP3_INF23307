using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Nordik_Aventure.Migrations
{
    /// <inheritdoc />
    public partial class AjouterResteProduits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "PaybackToSupplier", "PriceToBuy", "PriceToSell", "Sku", "Status", "SupplierId", "Weight" },
                values: new object[,]
                {
                    { 7, "Sac à dos 50 L etanche", 0.059999999999999998, 65.0, 139.0, "NC-SAC-001", "Actif", 4, 1.3 },
                    { 8, "Sac de jour 25 L", 0.059999999999999998, 32.0, 79.0, "NC-SAC-002", "Actif", 4, 0.90000000000000002 },
                    { 9, "Sac de couchage -10 degree", 0.029999999999999999, 80.0, 169.0, "NC-SAC-003", "Actif", 3, 2.2000000000000002 },
                    { 10, "Tapis autogonflant", 0.029999999999999999, 25.0, 59.0, "NC-SAC-004", "Actif", 3, 1.1000000000000001 },
                    { 11, "Housse impermeable sac a dos", 0.040000000000000001, 9.0, 19.0, "NC-SAC-005", "Actif", 2, 0.40000000000000002 },
                    { 12, "Batons de marche carbone", 0.040000000000000001, 35.0, 79.0, "NC-SAC-006", "Actif", 2, 0.80000000000000004 },
                    { 13, "Chandail thermique homme", 0.050000000000000003, 22.0, 59.0, "NC-VET-001", "Actif", 5, 0.59999999999999998 },
                    { 14, "Chandail thermique femme", 0.050000000000000003, 22.0, 59.0, "NC-VET-002", "Actif", 5, 0.59999999999999998 },
                    { 15, "Pantalon de randonnee homme", 0.050000000000000003, 38.0, 89.0, "NC-VET-003", "Actif", 5, 0.80000000000000004 },
                    { 16, "Pantalon de randonnee femme", 0.050000000000000003, 38.0, 89.0, "NC-VET-004", "Actif", 5, 0.80000000000000004 },
                    { 17, "Manteau coupe-vent", 0.040000000000000001, 55.0, 129.0, "NC-VET-005", "Actif", 6, 1.1000000000000001 },
                    { 18, "Tuque en laine merinos", 0.040000000000000001, 10.0, 29.0, "NC-VET-006", "Actif", 6, 0.29999999999999999 },
                    { 19, "Gants isolants Hiver+", 0.040000000000000001, 18.0, 45.0, "NC-VET-007", "Actif", 6, 0.5 },
                    { 20, "Rechaud portatif", 0.040000000000000001, 25.0, 59.0, "NC-ACC-001", "Actif", 2, 0.90000000000000002 },
                    { 21, "Bouteille isotherme 1L", 0.029999999999999999, 12.0, 29.0, "NC-ACC-002", "Actif", 3, 0.40000000000000002 },
                    { 22, "Lampe frontale 300 lumens", 0.050000000000000003, 14.0, 39.0, "NC-ACC-003", "Actif", 1, 0.20000000000000001 },
                    { 23, "Ensemble vaisselle 4 pers.", 0.040000000000000001, 20.0, 49.0, "NC-ACC-004", "Actif", 2, 1.2 },
                    { 24, "Filtre a eau compact", 0.050000000000000003, 28.0, 69.0, "NC-ACC-005", "Actif", 1, 0.69999999999999996 },
                    { 25, "Couteau multifonction", 0.040000000000000001, 15.0, 39.0, "NC-ACC-006", "Actif", 4, 0.5 },
                    { 26, "Montre GPS plein air", 0.040000000000000001, 120.0, 279.0, "NC-ELE-001", "Actif", 7, 0.90000000000000002 },
                    { 27, "Chargeur solaire 20W", 0.040000000000000001, 35.0, 79.0, "NC-ELE-002", "Actif", 7, 0.59999999999999998 },
                    { 28, "Boussole de précision", 0.040000000000000001, 9.0, 24.0, "NC-ELE-003", "Actif", 2, 0.20000000000000001 },
                    { 29, "Radio météo portable", 0.040000000000000001, 22.0, 49.0, "NC-ELE-004", "Actif", 7, 0.80000000000000004 },
                    { 30, "Lampe USB rechargeable", 0.040000000000000001, 11.0, 25.0, "NC-ELE-005", "Actif", 7, 0.29999999999999999 }
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { 2, 7 },
                    { 2, 8 },
                    { 2, 9 },
                    { 2, 10 },
                    { 2, 11 },
                    { 2, 12 },
                    { 3, 13 },
                    { 3, 14 },
                    { 3, 15 },
                    { 3, 16 },
                    { 3, 17 },
                    { 3, 18 },
                    { 3, 19 },
                    { 4, 20 },
                    { 4, 21 },
                    { 4, 22 },
                    { 4, 23 },
                    { 4, 24 },
                    { 4, 25 },
                    { 5, 26 },
                    { 5, 27 },
                    { 5, 28 },
                    { 5, 29 },
                    { 5, 30 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 2, 7 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 2, 8 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 2, 9 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 2, 10 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 2, 11 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 2, 12 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 3, 13 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 3, 14 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 3, 15 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 3, 16 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 3, 17 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 3, 18 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 3, 19 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 4, 20 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 4, 21 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 4, 22 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 4, 23 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 4, 24 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 4, 25 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 5, 26 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 5, 27 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 5, 28 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 5, 29 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 5, 30 });

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
        }
    }
}
