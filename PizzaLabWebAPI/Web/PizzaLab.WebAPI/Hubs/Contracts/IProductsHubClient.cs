namespace PizzaLab.WebAPI.Hubs.Contracts
{
    using Models.Products.ViewModels;
    using System.Threading.Tasks;   

    public interface IProductsHubClient
    {
        Task BroadcastProduct(ProductViewModel product);
    }
}
