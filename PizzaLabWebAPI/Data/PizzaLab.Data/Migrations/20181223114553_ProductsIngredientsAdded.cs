using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaLab.Data.Migrations
{
    public partial class ProductsIngredientsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Products_ProductId",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_ProductId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Ingredients");

            migrationBuilder.CreateTable(
                name: "ProductsIngredients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<string>(nullable: true),
                    IgredientId = table.Column<int>(nullable: false),
                    IngredientId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsIngredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductsIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductsIngredients_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductsIngredients_IngredientId",
                table: "ProductsIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsIngredients_ProductId",
                table: "ProductsIngredients",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductsIngredients");

            migrationBuilder.AddColumn<string>(
                name: "ProductId",
                table: "Ingredients",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_ProductId",
                table: "Ingredients",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Products_ProductId",
                table: "Ingredients",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
