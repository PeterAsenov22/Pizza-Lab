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

    public class IngredientsServiceTests
    {
        private readonly IRepository<Ingredient> _ingredientsRepository;
        private readonly IngredientsService _ingredientsService;

        public IngredientsServiceTests()
        {
            var options = new DbContextOptionsBuilder<PizzaLabDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var dbContext = new PizzaLabDbContext(options);

            var mapperProfile = new MappingConfiguration();
            var conf = new MapperConfiguration(cfg => cfg.AddProfile(mapperProfile));
            var mapper = new Mapper(conf);

            this._ingredientsRepository = new EfRepository<Ingredient>(dbContext);
            this._ingredientsService = new IngredientsService(_ingredientsRepository, mapper);
        }

        [Fact]
        public async Task CreateAsyncShouldCreateIngredientSuccessfully()
        {
            await _ingredientsService.CreateAsync("ham");

            Assert.Equal(1, _ingredientsRepository.All().Count());
            Assert.Equal("ham", _ingredientsRepository.All().First().Name);
        }

        [Fact]
        public async Task CreateRangeAsyncShouldCreateIngredientsSuccessfully()
        {
            await _ingredientsService.CreateRangeAsync(new string[]
            {
                "ham", "mushrooms"
            });

            Assert.Equal(2, _ingredientsRepository.All().Count());
            Assert.Equal("ham", _ingredientsRepository.All().First().Name);
            Assert.Equal("mushrooms", _ingredientsRepository.All().Last().Name);
        }

        [Fact]
        public void FindByNameShouldReturnNull()
        {
            var ingredient = _ingredientsService.FindByName("ham");
            Assert.Null(ingredient);
        }

        [Fact]
        public async Task FindByNameShouldReturnCorrectValues()
        {
            await _ingredientsService.CreateAsync("ham");

            var ingredient = _ingredientsService.FindByName("ham");
            Assert.Equal("ham", ingredient.Name);
        }

        [Fact]
        public void AllShouldReturnEmptyCollection()
        {
            var allIngredients = _ingredientsService.All().ToList();
            Assert.Empty(allIngredients);
        }

        [Fact]
        public async Task AllShouldReturnCorrectValues()
        {
            await _ingredientsService.CreateRangeAsync(new string[]
            {
                "ham", "mushrooms"
            });

            var allIngredients = _ingredientsService.All().ToList();

            Assert.Equal(2, allIngredients.Count);
            Assert.Equal("ham", allIngredients.First().Name);
            Assert.Equal("mushrooms", allIngredients.Last().Name);
        }

        [Fact]
        public async Task AnyShouldReturnTrue()
        {
            await _ingredientsService.CreateAsync("ham");

            Assert.True(_ingredientsService.Any());
        }

        [Fact]
        public void AnyShouldReturnFalse()
        {
            Assert.False(_ingredientsService.Any());
        }
    }
}
