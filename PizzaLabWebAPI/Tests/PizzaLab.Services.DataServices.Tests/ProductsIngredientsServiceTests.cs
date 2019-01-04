namespace PizzaLab.Services.DataServices.Tests
{
    using Data;
    using Data.Common;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class ProductsIngredientsServiceTests
    {
        private readonly IRepository<ProductsIngredients> _productsIngredientsRepository;
        private readonly ProductsIngredientsService _productsIngredientsService;

        public ProductsIngredientsServiceTests()
        {
            var options = new DbContextOptionsBuilder<PizzaLabDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var dbContext = new PizzaLabDbContext(options);

            this._productsIngredientsRepository = new EfRepository<ProductsIngredients>(dbContext);
            this._productsIngredientsService = new ProductsIngredientsService(_productsIngredientsRepository);
        }

        [Fact]
        public async Task DeleteProductIngredientsAsyncShouldDeleteProductIngredientsSuccessfully()
        {
            var productIngredients = new List<ProductsIngredients>()
            {
                new ProductsIngredients
                {
                    IngredientId = 1,
                    ProductId = "1234"
                },
                new ProductsIngredients
                {
                    IngredientId = 2,
                    ProductId = "1234"
                },
                new ProductsIngredients
                {
                    IngredientId = 3,
                    ProductId = "12345"
                }
            };

            await this._productsIngredientsRepository.AddRangeAsync(productIngredients);
            await this._productsIngredientsRepository.SaveChangesAsync();

            await this._productsIngredientsService.DeleteProductIngredientsAsync("1234");

            var productsIngredients = this._productsIngredientsRepository.All();
            Assert.Equal(1, productsIngredients.Count());

            var productIngredient = productsIngredients.First();
            Assert.Equal(3, productIngredient.IngredientId);
            Assert.Equal("12345", productIngredient.ProductId);
        }
    }
}
