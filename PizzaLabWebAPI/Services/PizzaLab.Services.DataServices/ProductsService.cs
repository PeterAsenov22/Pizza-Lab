namespace PizzaLab.Services.DataServices
{
    using Contracts;
    using Data.Common;
    using Data.Models;
    using Microsoft.EntityFrameworkCore.Internal;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ProductsService : IProductsService
    {
        private readonly IRepository<Product> _productsRepository;

        public ProductsService(IRepository<Product> productsRepository)
        {
            this._productsRepository = productsRepository;
        }

        public bool Any()
        {
            return this._productsRepository.All().Any();
        }

        public async Task CreateAsync(Product product)
        {
            await this._productsRepository.AddAsync(product);
            await this._productsRepository.SaveChangesAsync();
        }

        public async Task CreateRangeAsync(IEnumerable<Product> products)
        {
            await this._productsRepository.AddRangeAsync(products);
            await this._productsRepository.SaveChangesAsync();
        }
    }
}
