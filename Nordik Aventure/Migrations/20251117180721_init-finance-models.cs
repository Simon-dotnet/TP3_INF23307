using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Nordik_Aventure.Migrations
{
    /// <inheritdoc />
    public partial class initfinancemodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

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

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: 3);

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

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Taxes",
                columns: table => new
                {
                    ValueTps = table.Column<double>(type: "double", nullable: false),
                    ValueTvq = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Amount = table.Column<double>(type: "double", nullable: false),
                    AmountTps = table.Column<double>(type: "double", nullable: false),
                    AmountTvq = table.Column<double>(type: "double", nullable: false),
                    AmountTotal = table.Column<double>(type: "double", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.TransactionId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TransactionId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<double>(type: "double", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_Transaction_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transaction",
                        principalColumn: "TransactionId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    PurchaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.PurchaseId);
                    table.ForeignKey(
                        name: "FK_Purchases_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Purchases_Transaction_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transaction",
                        principalColumn: "TransactionId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    SaleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.SaleId);
                    table.ForeignKey(
                        name: "FK_Sale_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sale_Transaction_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transaction",
                        principalColumn: "TransactionId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TransactionHistory",
                columns: table => new
                {
                    TransactionHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionHistory", x => x.TransactionHistoryId);
                    table.ForeignKey(
                        name: "FK_TransactionHistory_Transaction_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transaction",
                        principalColumn: "TransactionId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PurchaseDetails",
                columns: table => new
                {
                    PurchaseDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    PurchaseId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseDetails", x => x.PurchaseDetailsId);
                    table.ForeignKey(
                        name: "FK_PurchaseDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseDetails_Purchases_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "Purchases",
                        principalColumn: "PurchaseId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SupplierReceipt",
                columns: table => new
                {
                    SupplierReceiptId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PurchaseId = table.Column<int>(type: "int", nullable: false),
                    PaymentId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierReceipt", x => x.SupplierReceiptId);
                    table.ForeignKey(
                        name: "FK_SupplierReceipt_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "PaymentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupplierReceipt_Purchases_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "Purchases",
                        principalColumn: "PurchaseId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SaleDetails",
                columns: table => new
                {
                    SaleDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    SaleId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleDetails", x => x.SaleDetailsId);
                    table.ForeignKey(
                        name: "FK_SaleDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleDetails_Sale_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sale",
                        principalColumn: "SaleId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SaleReceipt",
                columns: table => new
                {
                    SaleReceiptId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SaleId = table.Column<int>(type: "int", nullable: false),
                    PaymentId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleReceipt", x => x.SaleReceiptId);
                    table.ForeignKey(
                        name: "FK_SaleReceipt_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "PaymentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleReceipt_Sale_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sale",
                        principalColumn: "SaleId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_TransactionId",
                table: "Payments",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDetails_ProductId",
                table: "PurchaseDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDetails_PurchaseId",
                table: "PurchaseDetails",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_OrderId",
                table: "Purchases",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_TransactionId",
                table: "Purchases",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_ClientId",
                table: "Sale",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_TransactionId",
                table: "Sale",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetails_ProductId",
                table: "SaleDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetails_SaleId",
                table: "SaleDetails",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleReceipt_PaymentId",
                table: "SaleReceipt",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleReceipt_SaleId",
                table: "SaleReceipt",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierReceipt_PaymentId",
                table: "SupplierReceipt",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierReceipt_PurchaseId",
                table: "SupplierReceipt",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistory_TransactionId",
                table: "TransactionHistory",
                column: "TransactionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseDetails");

            migrationBuilder.DropTable(
                name: "SaleDetails");

            migrationBuilder.DropTable(
                name: "SaleReceipt");

            migrationBuilder.DropTable(
                name: "SupplierReceipt");

            migrationBuilder.DropTable(
                name: "Taxes");

            migrationBuilder.DropTable(
                name: "TransactionHistory");

            migrationBuilder.DropTable(
                name: "Sale");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Tentes & abris" },
                    { 2, "Sacs & portage" },
                    { 3, "Vetements techniques" },
                    { 4, "Accessoires & cuisine" },
                    { 5, "Electronique & navigation" }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Address", "Email", "Name", "Password", "Phone", "SatisfactionLevel", "Status", "Type" },
                values: new object[,]
                {
                    { 1, "144 rue de paul, Lévis, Qc, Canada", "paul@paul.ca", "Paul", "Paul123", "418-878-4090", null, null, "particulier" },
                    { 2, "123 rue du kayak, Montréal, Qc, Canada", "kayak@kayak.ca", "KayakManiac", "Kayak123", "418-878-4990", null, null, "entreprise" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "EmailAddress", "HireDate", "Name", "Password", "PhoneNumber", "Role", "Surname" },
                values: new object[,]
                {
                    { 1, "marc123@gmail.com", new DateTime(2025, 11, 12, 0, 0, 0, 0, DateTimeKind.Local), "Marc", "marc123*", "418-882-8636", "Employee", "Leblond" },
                    { 2, "jean123@gmail.com", new DateTime(2025, 11, 12, 0, 0, 0, 0, DateTimeKind.Local), "Jean", "jean123*", "418-882-8646", "Manager", "Laronde" },
                    { 3, "money@gmail.com", new DateTime(2025, 11, 12, 0, 0, 0, 0, DateTimeKind.Local), "Arjean", "money123*", "418-182-8646", "Accountant", "Labonde" }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "AverageDeliveryTime", "Code", "Discount", "Name" },
                values: new object[,]
                {
                    { 1, "1 jour", "AX", 0, "AventureX" },
                    { 2, "5 jour", "TS", 0, "TrekSupply" },
                    { 3, "6 jour", "MN", 0, "MontNord" },
                    { 4, "3 jour", "NP", 0, "NordPack" },
                    { 5, "4 jour", "NW", 0, "NordWear" },
                    { 6, "2 jour", "AL", 0, "ArcticLine" },
                    { 7, "2 jour", "TT", 0, "TechTrail" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Name", "PaybackToSupplier", "PriceToBuy", "PriceToSell", "Sku", "Status", "SupplierId", "Weight" },
                values: new object[,]
                {
                    { 1, 1, "Tente légère 2 places", 0.050000000000000003, 145.0, 299.0, "NC-TNT-001", "Actif", 1, 2.7999999999999998 },
                    { 2, 1, "Tente familiale 6 places", 0.050000000000000003, 260.0, 499.0, "NC-TNT-002", "Actif", 1, 6.5 },
                    { 3, 1, "Toile imperméable 3x3 m", 0.040000000000000001, 25.0, 59.0, "NC-TNT-003", "Actif", 2, 1.1000000000000001 },
                    { 4, 1, "Tapis de sol isolant", 0.029999999999999999, 18.0, 39.0, "NC-TNT-004", "Actif", 3, 0.90000000000000002 },
                    { 5, 1, "Abri cuisine pliable", 0.050000000000000003, 75.0, 149.0, "NC-TNT-005", "Actif", 1, 5.0 },
                    { 6, 1, "Mat telescopique alu", 0.040000000000000001, 12.0, 29.0, "NC-TNT-006", "Actif", 2, 0.69999999999999996 },
                    { 7, 2, "Sac à dos 50 L etanche", 0.059999999999999998, 65.0, 139.0, "NC-SAC-001", "Actif", 4, 1.3 },
                    { 8, 2, "Sac de jour 25 L", 0.059999999999999998, 32.0, 79.0, "NC-SAC-002", "Actif", 4, 0.90000000000000002 },
                    { 9, 2, "Sac de couchage -10 degree", 0.029999999999999999, 80.0, 169.0, "NC-SAC-003", "Actif", 3, 2.2000000000000002 },
                    { 10, 2, "Tapis autogonflant", 0.029999999999999999, 25.0, 59.0, "NC-SAC-004", "Actif", 3, 1.1000000000000001 },
                    { 11, 2, "Housse impermeable sac a dos", 0.040000000000000001, 9.0, 19.0, "NC-SAC-005", "Actif", 2, 0.40000000000000002 },
                    { 12, 2, "Batons de marche carbone", 0.040000000000000001, 35.0, 79.0, "NC-SAC-006", "Actif", 2, 0.80000000000000004 },
                    { 13, 3, "Chandail thermique homme", 0.050000000000000003, 22.0, 59.0, "NC-VET-001", "Actif", 5, 0.59999999999999998 },
                    { 14, 3, "Chandail thermique femme", 0.050000000000000003, 22.0, 59.0, "NC-VET-002", "Actif", 5, 0.59999999999999998 },
                    { 15, 3, "Pantalon de randonnee homme", 0.050000000000000003, 38.0, 89.0, "NC-VET-003", "Actif", 5, 0.80000000000000004 },
                    { 16, 3, "Pantalon de randonnee femme", 0.050000000000000003, 38.0, 89.0, "NC-VET-004", "Actif", 5, 0.80000000000000004 },
                    { 17, 3, "Manteau coupe-vent", 0.040000000000000001, 55.0, 129.0, "NC-VET-005", "Actif", 6, 1.1000000000000001 },
                    { 18, 3, "Tuque en laine merinos", 0.040000000000000001, 10.0, 29.0, "NC-VET-006", "Actif", 6, 0.29999999999999999 },
                    { 19, 3, "Gants isolants Hiver+", 0.040000000000000001, 18.0, 45.0, "NC-VET-007", "Actif", 6, 0.5 },
                    { 20, 4, "Rechaud portatif", 0.040000000000000001, 25.0, 59.0, "NC-ACC-001", "Actif", 2, 0.90000000000000002 },
                    { 21, 4, "Bouteille isotherme 1L", 0.029999999999999999, 12.0, 29.0, "NC-ACC-002", "Actif", 3, 0.40000000000000002 },
                    { 22, 4, "Lampe frontale 300 lumens", 0.050000000000000003, 14.0, 39.0, "NC-ACC-003", "Actif", 1, 0.20000000000000001 },
                    { 23, 4, "Ensemble vaisselle 4 pers.", 0.040000000000000001, 20.0, 49.0, "NC-ACC-004", "Actif", 2, 1.2 },
                    { 24, 4, "Filtre a eau compact", 0.050000000000000003, 28.0, 69.0, "NC-ACC-005", "Actif", 1, 0.69999999999999996 },
                    { 25, 4, "Couteau multifonction", 0.040000000000000001, 15.0, 39.0, "NC-ACC-006", "Actif", 4, 0.5 },
                    { 26, 5, "Montre GPS plein air", 0.040000000000000001, 120.0, 279.0, "NC-ELE-001", "Actif", 7, 0.90000000000000002 },
                    { 27, 5, "Chargeur solaire 20W", 0.040000000000000001, 35.0, 79.0, "NC-ELE-002", "Actif", 7, 0.59999999999999998 },
                    { 28, 5, "Boussole de précision", 0.040000000000000001, 9.0, 24.0, "NC-ELE-003", "Actif", 2, 0.20000000000000001 },
                    { 29, 5, "Radio météo portable", 0.040000000000000001, 22.0, 49.0, "NC-ELE-004", "Actif", 7, 0.80000000000000004 },
                    { 30, 5, "Lampe USB rechargeable", 0.040000000000000001, 11.0, 25.0, "NC-ELE-005", "Actif", 7, 0.29999999999999999 }
                });
        }
    }
}
