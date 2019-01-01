namespace PizzaLab.Services.DataServices.Tests
{
    using AutoMapper;
    using Data;
    using Data.Common;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using WebAPI.Helpers;
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

            var mapperProfile = new MappingConfiguration();
            var conf = new MapperConfiguration(cfg => cfg.AddProfile(mapperProfile));
            var mapper = new Mapper(conf);

            this._categoriesRepository = new EfRepository<Category>(dbContext);            
            this._categoriesService = new CategoriesService(_categoriesRepository, mapper);
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

        [Fact]
        public void FindByNameShouldReturnNull()
        {
            var category = _categoriesService.FindByName("Traditional");
            Assert.Null(category);
        }

        [Fact]
        public async Task FindByNameShouldReturnCorrectValues()
        {
            await _categoriesService.CreateAsync("Traditional");

            var category = _categoriesService.FindByName("Traditional");
            Assert.Equal("Traditional", category.Name);
        }

        [Fact]
        public void AllShouldReturnEmptyCollection()
        {
            var allCategories = _categoriesService.All().ToList();
            Assert.Empty(allCategories);
        }

        [Fact]
        public async Task AllShouldReturnCorrectValues()
        {
            await _categoriesService.CreateRangeAsync(new string[]
            {
                "Traditional", "Vegan"
            });

            var allCategories = _categoriesService.All().ToList();
            Assert.Equal(2, allCategories.Count);
            Assert.Equal("Traditional", allCategories.First().Name);
            Assert.Equal("Vegan", allCategories.Last().Name);
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
    }
}
