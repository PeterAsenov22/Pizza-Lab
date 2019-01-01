namespace PizzaLab.Services.DataServices.Contracts
{
    using Models.Products;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductsService
    {
        IEnumerable<ProductDto> All();

        bool Any();

        Task<ProductDto> CreateAsync(ProductDto product);

        Task CreateRangeAsync(IEnumerable<ProductDto> products);

        Task DeleteAsync(string productId);

        Task<ProductDto> EditAsync(ProductDto product);

        bool Exists(string productId);
    }
}
