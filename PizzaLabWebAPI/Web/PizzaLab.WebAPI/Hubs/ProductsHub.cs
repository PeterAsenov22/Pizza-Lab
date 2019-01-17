namespace PizzaLab.WebAPI.Hubs
{
    using Contracts;
    using Microsoft.AspNetCore.SignalR;

    public class ProductsHub : Hub<IProductsHubClient>
    {
    }
}
