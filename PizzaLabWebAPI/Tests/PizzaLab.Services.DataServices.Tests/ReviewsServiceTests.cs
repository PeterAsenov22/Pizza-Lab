namespace PizzaLab.Services.DataServices.Tests
{
    using Contracts;
    using Data;
    using Data.Common;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class ReviewsServiceTests
    {
        private readonly IRepository<Review> _reviewsRepository;
        private readonly IReviewsService _reviewsService;

        public ReviewsServiceTests()
        {
            var options = new DbContextOptionsBuilder<PizzaLabDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var dbContext = new PizzaLabDbContext(options);
            this._reviewsRepository = new EfRepository<Review>(dbContext);
            this._reviewsService = new ReviewsService(_reviewsRepository);
        }

        [Fact]
        public async Task CreateAsyncShouldCreateReviewSuccessfully()
        {
            await _reviewsService.CreateAsync("Very good pizza", "1234", "2345");

            Assert.Equal(1, _reviewsRepository.All().Count());
            Assert.Equal("Very good pizza", _reviewsRepository.All().First().Text);
            Assert.Equal("1234", _reviewsRepository.All().First().CreatorId);
            Assert.Equal("2345", _reviewsRepository.All().First().ProductId);
        }

        [Fact]
        public async Task GetProductReviewsShouldReturnCorrectResults()
        {
            await _reviewsService.CreateAsync("Very good pizza", "1234", "2345");
            await _reviewsService.CreateAsync("Brilliant", "12345", "2345");

            var productReviews = _reviewsService.GetProductReviews("2345");

            Assert.Equal(2, productReviews.Count());
            Assert.Equal("Very good pizza", _reviewsRepository.All().First().Text);
            Assert.Equal("1234", _reviewsRepository.All().First().CreatorId);
            Assert.Equal("2345", _reviewsRepository.All().First().ProductId);
            Assert.Equal("Brilliant", _reviewsRepository.All().Last().Text);
            Assert.Equal("12345", _reviewsRepository.All().Last().CreatorId);
            Assert.Equal("2345", _reviewsRepository.All().Last().ProductId);
        }
    }
}
