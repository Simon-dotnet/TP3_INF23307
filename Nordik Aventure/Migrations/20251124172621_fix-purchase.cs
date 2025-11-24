using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nordik_Aventure.Migrations
{
    /// <inheritdoc />
    public partial class fixpurchase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Orders_OrderId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_OrderId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Purchases");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Purchases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_OrderId",
                table: "Purchases",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Orders_OrderId",
                table: "Purchases",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
