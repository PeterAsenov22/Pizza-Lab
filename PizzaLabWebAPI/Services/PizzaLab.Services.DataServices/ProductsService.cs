namespace PizzaLab.Services.DataServices
{   
    using Contracts;
    using Data.Common;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProductsService : IProductsService
    {
        private readonly IRepository<Product> _productsRepository;

        public ProductsService(
            IRepository<Product> productsRepository)
        {
            this._productsRepository = productsRepository;
        }

        public IEnumerable<Product> All()
        {
            return this._productsRepository
                .All()
                .Include(p => p.Category)
                .Include(p => p.Ingredients)
                .ThenInclude(pi => pi.Ingredient)
                .Include(p => p.Likes)
                .ThenInclude(ul => ul.ApplicationUser);
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

        public async Task DeleteAsync(string productId)
        {
            var product = this._productsRepository
                .All()
                .First(p => p.Id == productId);

            this._productsRepository.Delete(product);
            await this._productsRepository.SaveChangesAsync();
        }

        public async Task EditAsync(Product product)
        {
            this._productsRepository.Update(product);
            await this._productsRepository.SaveChangesAsync();
        }

        public bool Exists(string productId)
        {
            return this._productsRepository
                .All()
                .Any(p => p.Id == productId);
        }
    }
}
