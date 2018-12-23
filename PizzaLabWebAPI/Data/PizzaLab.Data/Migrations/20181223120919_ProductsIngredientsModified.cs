using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaLab.Data.Migrations
{
    public partial class ProductsIngredientsModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsIngredients_Ingredients_IngredientId",
                table: "ProductsIngredients");

            migrationBuilder.DropColumn(
                name: "IgredientId",
                table: "ProductsIngredients");

            migrationBuilder.AlterColumn<int>(
                name: "IngredientId",
                table: "ProductsIngredients",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsIngredients_Ingredients_IngredientId",
                table: "ProductsIngredients",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsIngredients_Ingredients_IngredientId",
                table: "ProductsIngredients");

            migrationBuilder.AlterColumn<int>(
                name: "IngredientId",
                table: "ProductsIngredients",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "IgredientId",
                table: "ProductsIngredients",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsIngredients_Ingredients_IngredientId",
                table: "ProductsIngredients",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
