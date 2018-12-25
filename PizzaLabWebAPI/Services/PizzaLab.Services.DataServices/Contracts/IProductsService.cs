namespace PizzaLab.Services.DataServices.Contracts
{
    using Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;    

    public interface IProductsService
    {
        IEnumerable<Product> All();

        bool Any();

        Task CreateAsync(Product product);

        Task CreateRangeAsync(IEnumerable<Product> products);

        bool Exists(string productId);
    }
}
