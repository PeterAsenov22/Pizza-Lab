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

    public class ProductsServiceTests
    {
        private readonly IRepository<Product> _productsRepository;
        private readonly ProductsService _productsService;

        public ProductsServiceTests()
        {
            var options = new DbContextOptionsBuilder<PizzaLabDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var dbContext = new PizzaLabDbContext(options);
            this._productsRepository = new EfRepository<Product>(dbContext);
            this._productsService = new ProductsService(_productsRepository);
        }

        [Fact]
        public async Task AnyShouldReturnTrue()
        {
            var product = new Product
            {
                Name = "Diablo"
            };

            await _productsService.CreateAsync(product);

            Assert.True(_productsService.Any());
        }

        [Fact]
        public void AnyShouldReturnFalse()
        {
            Assert.False(_productsService.Any());
        }

        [Fact]
        public async Task CreateAsyncShouldCreateProductSuccessfully()
        {
            var diablo = new Product
            {
                Name = "Diablo",
                Description = "Pizza diavola means the devils pizza and is quite a spicy little devil and one of my favourite pizzas. If you like spicy hot and chilli flavours you will enjoy this pizza.",
                Price = 8.90m,
                Weight = 500,
                Image = "https://images.pizza33.ua/products/product/yQfkJqZweoLn9omo68oz5BnaGzaIE0UJ.jpg"
            };

            await _productsService.CreateAsync(diablo);

            Assert.Equal(1, _productsRepository.All().Count());

            var product = _productsRepository.All().First();
            Assert.Equal("Diablo", product.Name);
            Assert.Equal("Pizza diavola means the devils pizza and is quite a spicy little devil and one of my favourite pizzas. If you like spicy hot and chilli flavours you will enjoy this pizza.", product.Description);
            Assert.Equal(8.90m, product.Price);
            Assert.Equal(500, product.Weight);
            Assert.Equal("https://images.pizza33.ua/products/product/yQfkJqZweoLn9omo68oz5BnaGzaIE0UJ.jpg", product.Image);
        }

        [Fact]
        public async Task CreateRangeAsyncShouldCreateProductsSuccessfully()
        {
            var products = new List<Product>();

            var pollo = new Product
            {
                Name = "Pollo",
                Description = "Pollo might be your choice when you are in the mood for something healthy. Tender grilled chicken, creamy feta, roasted red peppers and corn are generously piled on top of our famous tomato sauce.",
                Price = 10.90m,
                Weight = 550,
                Image = "http://www.ilforno.bg/45-large_default/polo.jpg"
            };
            products.Add(pollo);

            var diablo = new Product
            {
                Name = "Diablo",
                Description = "Pizza diavola means the devils pizza and is quite a spicy little devil and one of my favourite pizzas. If you like spicy hot and chilli flavours you will enjoy this pizza.",
                Price = 8.90m,
                Weight = 500,
                Image = "https://images.pizza33.ua/products/product/yQfkJqZweoLn9omo68oz5BnaGzaIE0UJ.jpg"
            };
            products.Add(diablo);

            await _productsService.CreateRangeAsync(products);

            Assert.Equal(2, _productsRepository.All().Count());

            var firstProduct = _productsRepository.All().First();
            Assert.Equal("Pollo", firstProduct.Name);
            Assert.Equal("Pollo might be your choice when you are in the mood for something healthy. Tender grilled chicken, creamy feta, roasted red peppers and corn are generously piled on top of our famous tomato sauce.", firstProduct.Description);
            Assert.Equal(10.90m, firstProduct.Price);
            Assert.Equal(550, firstProduct.Weight);
            Assert.Equal("http://www.ilforno.bg/45-large_default/polo.jpg", firstProduct.Image);

            var secondProduct = _productsRepository.All().Last();
            Assert.Equal("Diablo", secondProduct.Name);
            Assert.Equal("Pizza diavola means the devils pizza and is quite a spicy little devil and one of my favourite pizzas. If you like spicy hot and chilli flavours you will enjoy this pizza.", secondProduct.Description);
            Assert.Equal(8.90m, secondProduct.Price);
            Assert.Equal(500, secondProduct.Weight);
            Assert.Equal("https://images.pizza33.ua/products/product/yQfkJqZweoLn9omo68oz5BnaGzaIE0UJ.jpg", secondProduct.Image);
        }
    }
}
