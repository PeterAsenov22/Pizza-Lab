namespace PizzaLab.Services.DataServices.Contracts
{
    using System.Threading.Tasks;

    public interface IUsersLikesService
    {
        Task CreateUserLikeAsync(string productId, string userId);

        Task DeleteProductLikesAsync(string productId);

        Task DeleteUserLikeAsync(string productId, string userId);
    }
}
