namespace PizzaLab.Services.DataServices.Tests
{
    using Data;
    using Data.Common;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class CategoriesServiceTests
    {
        private readonly IRepository<Category> _categoriesRepository;
        private readonly CategoriesService _categoriesService;

        public CategoriesServiceTests()
        {
            var options = new DbContextOptionsBuilder<PizzaLabDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var dbContext = new PizzaLabDbContext(options);
            this._categoriesRepository = new EfRepository<Category>(dbContext);
            this._categoriesService = new CategoriesService(_categoriesRepository);
        }

        [Fact]
        public async Task AnyShouldReturnTrue()
        {
            await _categoriesService.CreateAsync("Traditional");

            Assert.True(_categoriesService.Any());
        }

        [Fact]
        public void AnyShouldReturnFalse()
        {           
            Assert.False(_categoriesService.Any());
        }

        [Fact]
        public async Task CreateAsyncShouldCreateCategorySuccessfully()
        {
            await _categoriesService.CreateAsync("Traditional");

            Assert.Equal(1, _categoriesRepository.All().Count());
            Assert.Equal("Traditional", _categoriesRepository.All().First().Name);
        }

        [Fact]
        public async Task CreateRangeAsyncShouldCreateCategorySuccessfully()
        {
            await _categoriesService.CreateRangeAsync(new string[]
            {
                "Traditional", "Vegan"
            });

            Assert.Equal(2, _categoriesRepository.All().Count());
            Assert.Equal("Traditional", _categoriesRepository.All().First().Name);
            Assert.Equal("Vegan", _categoriesRepository.All().Last().Name);
        }
    }
}
