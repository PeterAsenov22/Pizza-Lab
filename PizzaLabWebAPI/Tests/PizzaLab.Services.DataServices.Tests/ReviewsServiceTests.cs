namespace PizzaLab.Services.DataServices.Tests
{
    using AutoMapper; 
    using Contracts;
    using Data;
    using Data.Common;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using WebAPI.Helpers;
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

            var mapperProfile = new MappingConfiguration();
            var conf = new MapperConfiguration(cfg => cfg.AddProfile(mapperProfile));
            var mapper = new Mapper(conf);

            this._reviewsRepository = new EfRepository<Review>(dbContext);
            this._reviewsService = new ReviewsService(_reviewsRepository, mapper);
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
        public void GetProductReviewsShouldReturnEmptyCollection()
        {
            var productReviews = _reviewsService.GetProductReviews("2345");
            Assert.Empty(productReviews);
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

        [Fact]
        public async Task DeleteProductReviewsAsyncShouldDeleteProductReviewsSuccessfully()
        {
            await _reviewsService.CreateAsync("Very good pizza", "1234", "2345");
            await _reviewsService.CreateAsync("Brilliant", "12345", "2345");

            var productReviews = _reviewsService.GetProductReviews("2345");
            Assert.Equal(2, productReviews.Count());

            await _reviewsService.DeleteProductReviewsAsync("2345");
            productReviews = _reviewsService.GetProductReviews("2345");
            Assert.Empty(productReviews);
        }

        [Fact]
        public async Task DeleteReviewAsyncShouldDeleteReviewSuccessfully()
        {
            await _reviewsService.CreateAsync("Very good pizza", "1234", "2345");
            await _reviewsService.CreateAsync("Brilliant", "12345", "2345");

            var reviewId = _reviewsRepository.All().First().Id;
            await _reviewsService.DeleteReviewAsync(reviewId);

            Assert.Equal(1, _reviewsRepository.All().Count());
            Assert.Equal("Brilliant", _reviewsRepository.All().First().Text);
        }

        [Fact]
        public async Task ExistsShouldReturnTrue()
        {
            await _reviewsService.CreateAsync("Very good pizza", "1234", "2345");
            await _reviewsService.CreateAsync("Brilliant", "12345", "2345");

            var firstReviewId = _reviewsRepository.All().First().Id;
            var secondReviewId = _reviewsRepository.All().Last().Id;

            Assert.True(_reviewsService.Exists(firstReviewId));
            Assert.True(_reviewsService.Exists(secondReviewId));
        }

        [Fact]
        public async Task ExistsShouldReturnFalse()
        {
            await _reviewsService.CreateAsync("Very good pizza", "1234", "2345");
            await _reviewsService.CreateAsync("Brilliant", "12345", "2345");

            Assert.False(_reviewsService.Exists("1234"));
        }

        [Fact]
        public async Task FindReviewCreatorByIdShouldReturnCorrectReviewCreatorUsername()
        {
            var review = new Review
            {
                Id = "reviewId",
                Creator = new ApplicationUser
                {
                    Id = "testId",
                    UserName = "TestUsername"
                },
                ProductId = "1234"
            };
            await this._reviewsRepository.AddAsync(review);
            await this._reviewsRepository.SaveChangesAsync();

            var creatorUsername = _reviewsService.FindReviewCreatorById("reviewId");
            Assert.Equal("TestUsername", creatorUsername);
        }
    }
}
