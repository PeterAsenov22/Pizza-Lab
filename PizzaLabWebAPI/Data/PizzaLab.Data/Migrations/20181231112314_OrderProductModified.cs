using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaLab.Data.Migrations
{
    public partial class OrderProductModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProduct_Products_ProductId1",
                table: "OrderProduct");

            migrationBuilder.DropIndex(
                name: "IX_OrderProduct_ProductId1",
                table: "OrderProduct");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "OrderProduct");

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "OrderProduct",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_ProductId",
                table: "OrderProduct",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProduct_Products_ProductId",
                table: "OrderProduct",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProduct_Products_ProductId",
                table: "OrderProduct");

            migrationBuilder.DropIndex(
                name: "IX_OrderProduct_ProductId",
                table: "OrderProduct");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "OrderProduct",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductId1",
                table: "OrderProduct",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_ProductId1",
                table: "OrderProduct",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProduct_Products_ProductId1",
                table: "OrderProduct",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
