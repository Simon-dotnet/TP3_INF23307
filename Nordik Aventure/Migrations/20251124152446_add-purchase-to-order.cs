using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nordik_Aventure.Migrations
{
    /// <inheritdoc />
    public partial class addpurchasetoorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleDetails_Products_ProductId",
                table: "SaleDetails");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "SaleDetails",
                newName: "ProductStockId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleDetails_ProductId",
                table: "SaleDetails",
                newName: "IX_SaleDetails_ProductStockId");

            migrationBuilder.AddColumn<int>(
                name: "PurchaseId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MovementHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Motif = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PurchaseId = table.Column<int>(type: "int", nullable: true),
                    SaleId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovementHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovementHistory_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovementHistory_Purchases_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "Purchases",
                        principalColumn: "PurchaseId");
                    table.ForeignKey(
                        name: "FK_MovementHistory_Sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sales",
                        principalColumn: "SaleId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PurchaseId",
                table: "Orders",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_MovementHistory_EmployeeId",
                table: "MovementHistory",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_MovementHistory_PurchaseId",
                table: "MovementHistory",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_MovementHistory_SaleId",
                table: "MovementHistory",
                column: "SaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Purchases_PurchaseId",
                table: "Orders",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "PurchaseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleDetails_ProductInStock_ProductStockId",
                table: "SaleDetails",
                column: "ProductStockId",
                principalTable: "ProductInStock",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Purchases_PurchaseId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleDetails_ProductInStock_ProductStockId",
                table: "SaleDetails");

            migrationBuilder.DropTable(
                name: "MovementHistory");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PurchaseId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PurchaseId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "ProductStockId",
                table: "SaleDetails",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleDetails_ProductStockId",
                table: "SaleDetails",
                newName: "IX_SaleDetails_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleDetails_Products_ProductId",
                table: "SaleDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
