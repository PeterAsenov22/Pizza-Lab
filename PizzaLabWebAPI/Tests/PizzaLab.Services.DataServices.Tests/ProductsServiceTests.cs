namespace PizzaLab.Services.DataServices.Tests
{
    using AutoMapper;
    using Data;
    using Data.Common;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models.Products;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WebAPI.Helpers;
    using Xunit;

    public class ProductsServiceTests
    {
        private readonly IRepository<Product> _productsRepository;
        private readonly ProductsService _productsService;
        private readonly PizzaLabDbContext _dbContext;

        public ProductsServiceTests()
        {
            var options = new DbContextOptionsBuilder<PizzaLabDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _dbContext = new PizzaLabDbContext(options);

            var mapperProfile = new MappingConfiguration();
            var conf = new MapperConfiguration(cfg => cfg.AddProfile(mapperProfile));
            var mapper = new Mapper(conf);

            this._productsRepository = new EfRepository<Product>(_dbContext);
            this._productsService = new ProductsService(_productsRepository, mapper);
        }

        [Fact]
        public async Task CreateAsyncShouldCreateProductSuccessfully()
        {
            var diablo = new ProductDto
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
            var products = new List<ProductDto>();

            var pollo = new ProductDto
            {
                Name = "Pollo",
                Description = "Pollo might be your choice when you are in the mood for something healthy. Tender grilled chicken, creamy feta, roasted red peppers and corn are generously piled on top of our famous tomato sauce.",
                Price = 10.90m,
                Weight = 550,
                Image = "http://www.ilforno.bg/45-large_default/polo.jpg"
            };
            products.Add(pollo);

            var diablo = new ProductDto
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

        [Fact]
        public void AllShouldReturnEmptyCollection()
        {
            var allProducts = _productsService.All().ToList();
            Assert.Empty(allProducts);
        }

        [Fact]
        public async Task AllShouldReturnCorrectValues()
        {
            var products = new List<Product>();

            var pollo = new Product
            {
                Name = "Pollo",
                Description = "Pollo might be your choice when you are in the mood for something healthy. Tender grilled chicken, creamy feta, roasted red peppers and corn are generously piled on top of our famous tomato sauce.",
                Price = 10.90m,
                Weight = 550,
                Image = "http://www.ilforno.bg/45-large_default/polo.jpg",
                Category = new Category
                {
                    Id = 1,
                    Name = "Traditional"
                },
                Ingredients = new List<ProductsIngredients>
                {
                    new ProductsIngredients
                    {
                        IngredientId = 1,
                        Ingredient = new Ingredient
                        {
                            Name = "ham"
                        }
                    }
                },
                Reviews = new List<Review>(),
                Likes = new List<UsersLikes>()
            };

            var diablo = new Product
            {
                Name = "Diablo",
                Description = "Pizza diavola means the devils pizza and is quite a spicy little devil and one of my favourite pizzas. If you like spicy hot and chilli flavours you will enjoy this pizza.",
                Price = 8.90m,
                Weight = 500,
                Image = "https://images.pizza33.ua/products/product/yQfkJqZweoLn9omo68oz5BnaGzaIE0UJ.jpg",
                Category = new Category
                {
                    Id = 2,
                    Name = "Premium"
                },
                Ingredients = new List<ProductsIngredients>
                {
                    new ProductsIngredients
                    {
                        IngredientId = 1,
                        Ingredient = new Ingredient
                        {
                            Name = "ham"
                        }
                    }
                },
                Reviews = new List<Review>(),
                Likes = new List<UsersLikes>()
            };

            products.Add(pollo);
            products.Add(diablo);

            await this._productsRepository.AddRangeAsync(products);
            await this._productsRepository.SaveChangesAsync();
   
            var allProducts = _productsService.All().ToList();
         
            Assert.Equal(2, allProducts.Count);
         
            var firstProduct = allProducts.First();
            Assert.Equal("Pollo", firstProduct.Name);
            Assert.Equal("Pollo might be your choice when you are in the mood for something healthy. Tender grilled chicken, creamy feta, roasted red peppers and corn are generously piled on top of our famous tomato sauce.", firstProduct.Description);
            Assert.Equal(10.90m, firstProduct.Price);
            Assert.Equal(550, firstProduct.Weight);
            Assert.Equal("http://www.ilforno.bg/45-large_default/polo.jpg", firstProduct.Image);
            Assert.Equal("Traditional", firstProduct.Category.Name);
         
            var secondProduct = allProducts.Last();
            Assert.Equal("Diablo", secondProduct.Name);
            Assert.Equal("Pizza diavola means the devils pizza and is quite a spicy little devil and one of my favourite pizzas. If you like spicy hot and chilli flavours you will enjoy this pizza.", secondProduct.Description);
            Assert.Equal(8.90m, secondProduct.Price);
            Assert.Equal(500, secondProduct.Weight);
            Assert.Equal("https://images.pizza33.ua/products/product/yQfkJqZweoLn9omo68oz5BnaGzaIE0UJ.jpg", secondProduct.Image);
            Assert.Equal("Premium", secondProduct.Category.Name);
        }

        [Fact]
        public async Task AnyShouldReturnTrue()
        {
            var product = new ProductDto
            {
                Name = "Pollo",
                CategoryId = 9,
                Description = "Pollo might be your choice when you are in the mood for something healthy. Tender grilled chicken, creamy feta, roasted red peppers and corn are generously piled on top of our famous tomato sauce.",
                Price = 10.90m,
                Weight = 550,
                Image = "http://www.ilforno.bg/45-large_default/polo.jpg"
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
        public void ExistsShouldReturnFalse()
        {
            Assert.False(_productsService.Exists("123456"));
        }

        [Fact]
        public async Task ExistsShouldReturnTrue()
        {
            var diablo = new ProductDto
            {
                Name = "Diablo",
                Description = "Pizza diavola means the devils pizza and is quite a spicy little devil and one of my favourite pizzas. If you like spicy hot and chilli flavours you will enjoy this pizza.",
                Price = 8.90m,
                Weight = 500,
                Image = "https://images.pizza33.ua/products/product/yQfkJqZweoLn9omo68oz5BnaGzaIE0UJ.jpg"
            };

            await _productsService.CreateAsync(diablo);
            var productId = _productsRepository.All().First().Id;

            Assert.True(_productsService.Exists(productId));
        }

        [Fact]
        public async Task EditAsyncShouldEditProductSuccessfully()
        {
            var product = new Product
            {
                Id = "1234",
                Name = "Pollo",
                Description = "Pollo might be your choice when you are in the mood for something healthy. Tender grilled chicken, creamy feta, roasted red peppers and corn are generously piled on top of our famous tomato sauce.",
                Price = 10.90m,
                Weight = 550,
                Image = "http://www.ilforno.bg/45-large_default/polo.jpg",
                Category = new Category
                {
                    Id = 1,
                    Name = "Traditional"
                },
                Ingredients = new List<ProductsIngredients>
                {
                    new ProductsIngredients
                    {
                        IngredientId = 1,
                        Ingredient = new Ingredient
                        {
                            Name = "ham"
                        }
                    }
                },
                Reviews = new List<Review>(),
                Likes = new List<UsersLikes>()
            };
            await this._productsRepository.AddAsync(product);
            await this._productsRepository.SaveChangesAsync();
            _dbContext.Entry(product).State = EntityState.Detached;

            var productDto = new ProductDto
            {
                Id = "1234",
                Name = "Diablo2",
                Description = "Pizza diavola means the devils pizza and is quite a spicy little devil and one of my favourite pizzas.",
                Price = 9.90m,
                Weight = 400,
                Image = "http://images.pizza33.ua/products/product/yQfkJqZweoLn9omo68oz5BnaGzaIE0UJ.jpg"
            };

            await _productsService.EditAsync(productDto);

            var editedProduct = _productsRepository.All().AsNoTracking().First();
            Assert.Equal("Diablo2", editedProduct.Name);
            Assert.Equal("Pizza diavola means the devils pizza and is quite a spicy little devil and one of my favourite pizzas.", editedProduct.Description);
            Assert.Equal(9.90m, editedProduct.Price);
            Assert.Equal(400, editedProduct.Weight);
            Assert.Equal("http://images.pizza33.ua/products/product/yQfkJqZweoLn9omo68oz5BnaGzaIE0UJ.jpg", editedProduct.Image);
        }

        [Fact]
        public async Task DeleteAsyncShouldDeleteProductSuccessfully()
        {
            var productDto = new ProductDto
            {
                Id = "1234",
                Name = "Diablo",
                Description = "Pizza diavola means the devils pizza and is quite a spicy little devil and one of my favourite pizzas. If you like spicy hot and chilli flavours you will enjoy this pizza.",
                Price = 8.90m,
                Weight = 500,
                Image = "https://images.pizza33.ua/products/product/yQfkJqZweoLn9omo68oz5BnaGzaIE0UJ.jpg"
            };

            await _productsService.CreateAsync(productDto);
            Assert.Equal(1, _productsRepository.All().Count());

            await _productsService.DeleteAsync(productDto.Id);
            Assert.Equal(0, _productsRepository.All().Count());
        }
    }
}
