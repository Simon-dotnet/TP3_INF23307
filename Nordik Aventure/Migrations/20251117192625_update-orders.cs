using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nordik_Aventure.Migrations
{
    /// <inheritdoc />
    public partial class updateorders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Transaction_TransactionId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Order_OrderId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Transaction_TransactionId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Sale_Clients_ClientId",
                table: "Sale");

            migrationBuilder.DropForeignKey(
                name: "FK_Sale_Transaction_TransactionId",
                table: "Sale");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleDetails_Sale_SaleId",
                table: "SaleDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleReceipt_Payments_PaymentId",
                table: "SaleReceipt");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleReceipt_Sale_SaleId",
                table: "SaleReceipt");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierReceipt_Payments_PaymentId",
                table: "SupplierReceipt");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierReceipt_Purchases_PurchaseId",
                table: "SupplierReceipt");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionHistory_Transaction_TransactionId",
                table: "TransactionHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierReceipt",
                table: "SupplierReceipt");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleReceipt",
                table: "SaleReceipt");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sale",
                table: "Sale");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "Transaction",
                newName: "Transactions");

            migrationBuilder.RenameTable(
                name: "SupplierReceipt",
                newName: "SupplierReceipts");

            migrationBuilder.RenameTable(
                name: "SaleReceipt",
                newName: "SaleReceipts");

            migrationBuilder.RenameTable(
                name: "Sale",
                newName: "Sales");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierReceipt_PurchaseId",
                table: "SupplierReceipts",
                newName: "IX_SupplierReceipts_PurchaseId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierReceipt_PaymentId",
                table: "SupplierReceipts",
                newName: "IX_SupplierReceipts_PaymentId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleReceipt_SaleId",
                table: "SaleReceipts",
                newName: "IX_SaleReceipts_SaleId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleReceipt_PaymentId",
                table: "SaleReceipts",
                newName: "IX_SaleReceipts_PaymentId");

            migrationBuilder.RenameIndex(
                name: "IX_Sale_TransactionId",
                table: "Sales",
                newName: "IX_Sales_TransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_Sale_ClientId",
                table: "Sales",
                newName: "IX_Sales_ClientId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfDelivery",
                table: "Orders",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfOrdering",
                table: "Orders",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "Orders",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "TransactionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierReceipts",
                table: "SupplierReceipts",
                column: "SupplierReceiptId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleReceipts",
                table: "SaleReceipts",
                column: "SaleReceiptId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sales",
                table: "Sales",
                column: "SaleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "OrderId");

            migrationBuilder.CreateTable(
                name: "OrderSupplierProducts",
                columns: table => new
                {
                    OrderSupplierProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<double>(type: "double", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSupplierProducts", x => x.OrderSupplierProductId);
                    table.ForeignKey(
                        name: "FK_OrderSupplierProducts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderSupplierProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderSupplierProducts_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSupplierProducts_OrderId",
                table: "OrderSupplierProducts",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSupplierProducts_ProductId",
                table: "OrderSupplierProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSupplierProducts_SupplierId",
                table: "OrderSupplierProducts",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Transactions_TransactionId",
                table: "Payments",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "TransactionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Orders_OrderId",
                table: "Purchases",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Transactions_TransactionId",
                table: "Purchases",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "TransactionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleDetails_Sales_SaleId",
                table: "SaleDetails",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "SaleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleReceipts_Payments_PaymentId",
                table: "SaleReceipts",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "PaymentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleReceipts_Sales_SaleId",
                table: "SaleReceipts",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "SaleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Clients_ClientId",
                table: "Sales",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Transactions_TransactionId",
                table: "Sales",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "TransactionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierReceipts_Payments_PaymentId",
                table: "SupplierReceipts",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "PaymentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierReceipts_Purchases_PurchaseId",
                table: "SupplierReceipts",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "PurchaseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionHistory_Transactions_TransactionId",
                table: "TransactionHistory",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "TransactionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Transactions_TransactionId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Orders_OrderId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Transactions_TransactionId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleDetails_Sales_SaleId",
                table: "SaleDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleReceipts_Payments_PaymentId",
                table: "SaleReceipts");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleReceipts_Sales_SaleId",
                table: "SaleReceipts");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Clients_ClientId",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Transactions_TransactionId",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierReceipts_Payments_PaymentId",
                table: "SupplierReceipts");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierReceipts_Purchases_PurchaseId",
                table: "SupplierReceipts");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionHistory_Transactions_TransactionId",
                table: "TransactionHistory");

            migrationBuilder.DropTable(
                name: "OrderSupplierProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierReceipts",
                table: "SupplierReceipts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sales",
                table: "Sales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleReceipts",
                table: "SaleReceipts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DateOfDelivery",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DateOfOrdering",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "Transaction");

            migrationBuilder.RenameTable(
                name: "SupplierReceipts",
                newName: "SupplierReceipt");

            migrationBuilder.RenameTable(
                name: "Sales",
                newName: "Sale");

            migrationBuilder.RenameTable(
                name: "SaleReceipts",
                newName: "SaleReceipt");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierReceipts_PurchaseId",
                table: "SupplierReceipt",
                newName: "IX_SupplierReceipt_PurchaseId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierReceipts_PaymentId",
                table: "SupplierReceipt",
                newName: "IX_SupplierReceipt_PaymentId");

            migrationBuilder.RenameIndex(
                name: "IX_Sales_TransactionId",
                table: "Sale",
                newName: "IX_Sale_TransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_Sales_ClientId",
                table: "Sale",
                newName: "IX_Sale_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleReceipts_SaleId",
                table: "SaleReceipt",
                newName: "IX_SaleReceipt_SaleId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleReceipts_PaymentId",
                table: "SaleReceipt",
                newName: "IX_SaleReceipt_PaymentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction",
                column: "TransactionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierReceipt",
                table: "SupplierReceipt",
                column: "SupplierReceiptId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sale",
                table: "Sale",
                column: "SaleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleReceipt",
                table: "SaleReceipt",
                column: "SaleReceiptId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Transaction_TransactionId",
                table: "Payments",
                column: "TransactionId",
                principalTable: "Transaction",
                principalColumn: "TransactionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Order_OrderId",
                table: "Purchases",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Transaction_TransactionId",
                table: "Purchases",
                column: "TransactionId",
                principalTable: "Transaction",
                principalColumn: "TransactionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sale_Clients_ClientId",
                table: "Sale",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sale_Transaction_TransactionId",
                table: "Sale",
                column: "TransactionId",
                principalTable: "Transaction",
                principalColumn: "TransactionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleDetails_Sale_SaleId",
                table: "SaleDetails",
                column: "SaleId",
                principalTable: "Sale",
                principalColumn: "SaleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleReceipt_Payments_PaymentId",
                table: "SaleReceipt",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "PaymentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleReceipt_Sale_SaleId",
                table: "SaleReceipt",
                column: "SaleId",
                principalTable: "Sale",
                principalColumn: "SaleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierReceipt_Payments_PaymentId",
                table: "SupplierReceipt",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "PaymentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierReceipt_Purchases_PurchaseId",
                table: "SupplierReceipt",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "PurchaseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionHistory_Transaction_TransactionId",
                table: "TransactionHistory",
                column: "TransactionId",
                principalTable: "Transaction",
                principalColumn: "TransactionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
