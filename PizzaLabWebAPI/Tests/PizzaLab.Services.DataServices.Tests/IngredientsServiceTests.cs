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
            this._ingredientsRepository = new EfRepository<Ingredient>(dbContext);
            this._ingredientsService = new IngredientsService(_ingredientsRepository);
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
        public async Task AllShouldReturnCorrectValues()
        {
            await _ingredientsService.CreateRangeAsync(new string[]
            {
                "ham", "mushrooms"
            });

            Assert.Equal(_ingredientsService.All(), _ingredientsRepository.All());
        }
    }
}
