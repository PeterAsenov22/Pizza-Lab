namespace PizzaLab.Services.DataServices
{
    using AutoMapper;
    using Contracts;
    using Data.Common;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models.Products;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    
    public class ProductsService : IProductsService
    {
        private readonly IRepository<Product> _productsRepository;
        private readonly IMapper _mapper;

        public ProductsService(
            IRepository<Product> productsRepository,
            IMapper mapper)
        {
            this._productsRepository = productsRepository;
            this._mapper = mapper;
        }

        public IEnumerable<ProductDto> All()
        {
            return this._productsRepository
                .All()
                .AsNoTracking()
                .Include(p => p.Category)
                .Include(p => p.Ingredients)
                .ThenInclude(pi => pi.Ingredient)
                .Include(p => p.Likes)
                .ThenInclude(ul => ul.ApplicationUser)
                .Select(p => this._mapper.Map<ProductDto>(p));
        }

        public bool Any()
        {
            return this._productsRepository.All().Any();
        }

        public async Task<ProductDto> CreateAsync(ProductDto productDto)
        {
            var product = this._mapper
                .Map<Product>(productDto);

            await this._productsRepository.AddAsync(product);
            await this._productsRepository.SaveChangesAsync();

            var createdProduct = this.All()
                .First(p => p.Id == product.Id);

            return this._mapper.Map<ProductDto>(createdProduct);
        }

        public async Task CreateRangeAsync(IEnumerable<ProductDto> productsDtos)
        {
            var products = productsDtos
                .Select(pdto => this._mapper.Map<Product>(pdto));

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

        public async Task<ProductDto> EditAsync(ProductDto productDto)
        {
            var product = this._mapper
                .Map<Product>(productDto);

            this._productsRepository.Update(product);
            await this._productsRepository.SaveChangesAsync();

            var editedProduct = this.All()
                .First(p => p.Id == product.Id);

            return this._mapper.Map<ProductDto>(editedProduct);
        }

        public bool Exists(string productId)
        {
            return this._productsRepository
                .All()
                .Any(p => p.Id == productId);
        }
    }
}
