namespace PizzaLab.Services.DataServices.Contracts
{
    using System.Threading.Tasks;

    public interface IProductsIngredientsService
    {
        Task DeleteProductIngredientsAsync(string productId);
    }
}
