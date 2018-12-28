namespace PizzaLab.Services.DataServices
{
    using Contracts;
    using Data.Common;
    using Data.Models;
    using System.Linq;
    using System.Threading.Tasks;  

    public class UsersLikesService : IUsersLikesService
    {
        private readonly IRepository<UsersLikes> _usersLikesRepository;

        public UsersLikesService(IRepository<UsersLikes> usersLikesRepository)
        {
            this._usersLikesRepository = usersLikesRepository;
        }

        public async Task CreateUserLike(string productId, string userId)
        {
            await this._usersLikesRepository.AddAsync(new UsersLikes
            {
                ApplicationUserId = userId,
                ProductId = productId
            });

            await this._usersLikesRepository.SaveChangesAsync();
        }

        public async Task DeleteProductLikesAsync(string productId)
        {
            var productLikes = this._usersLikesRepository
                .All()
                .Where(ul => ul.ProductId == productId)
                .ToList();

            if (productLikes.Any())
            {
                this._usersLikesRepository.DeleteRange(productLikes);
                await this._usersLikesRepository.SaveChangesAsync();
            }
        }

        public async Task DeleteUserLikeAsync(string productId, string userId)
        {
            var userLike = this._usersLikesRepository
                .All()
                .FirstOrDefault(ul => ul.ApplicationUserId == userId && ul.ProductId == productId);

            if (userLike != null)
            {
                this._usersLikesRepository.Delete(userLike);
                await this._usersLikesRepository.SaveChangesAsync();
            }
        }
    }
}
