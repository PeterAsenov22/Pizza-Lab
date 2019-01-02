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

    public class UsersLikesServiceTests
    {
        private readonly IRepository<UsersLikes> _usersLikesRepository;
        private readonly UsersLikesService _usersLikesService;

        public UsersLikesServiceTests()
        {
            var options = new DbContextOptionsBuilder<PizzaLabDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var dbContext = new PizzaLabDbContext(options);

            this._usersLikesRepository = new EfRepository<UsersLikes>(dbContext);
            this._usersLikesService = new UsersLikesService(_usersLikesRepository);
        }

        [Fact]
        public async Task CreateUserLikeAsyncShouldCreateUserLikeSuccessfully()
        {
            await this._usersLikesService.CreateUserLikeAsync("1234", "abcd");
            Assert.Equal(1, this._usersLikesRepository.All().Count());

            var userLike = this._usersLikesRepository.All().First();
            Assert.Equal("1234", userLike.ProductId);
            Assert.Equal("abcd", userLike.ApplicationUserId);
        }

        [Fact]
        public async Task DeleteProductLikesAsyncShouldDeleteProductLikesSuccessfully()
        {
            await this._usersLikesService.CreateUserLikeAsync("1234", "abcd");
            await this._usersLikesService.CreateUserLikeAsync("1234", "abcde");
            await this._usersLikesService.CreateUserLikeAsync("12345", "abcd");

            await this._usersLikesService.DeleteProductLikesAsync("1234");

            Assert.Equal(1, this._usersLikesRepository.All().Count());
            var userLike = this._usersLikesRepository.All().First();
            Assert.Equal("12345", userLike.ProductId);
        }

        [Fact]
        public async Task DeleteUserLikeAsyncShouldDeleteUserLikesSuccessfully()
        {
            await this._usersLikesService.CreateUserLikeAsync("1234", "abcd");
            await this._usersLikesService.CreateUserLikeAsync("12345", "abcd");

            await this._usersLikesService.DeleteUserLikeAsync("1234", "abcd");

            Assert.Equal(1, this._usersLikesRepository.All().Count());
            var userLike = this._usersLikesRepository.All().First();
            Assert.Equal("12345", userLike.ProductId);
        }
    }
}
