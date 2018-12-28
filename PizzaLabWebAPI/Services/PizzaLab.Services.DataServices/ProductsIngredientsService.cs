namespace PizzaLab.Services.DataServices
{
    using Contracts;
    using Data.Common;
    using Data.Models; 
    using System.Linq;
    using System.Threading.Tasks; 

    public class ProductsIngredientsService : IProductsIngredientsService
    {
        private readonly IRepository<ProductsIngredients> _productsIngredientsRepository;

        public ProductsIngredientsService(IRepository<ProductsIngredients> productsIngredientsRepository)
        {
            this._productsIngredientsRepository = productsIngredientsRepository;
        }

        public async Task DeleteProductIngredientsAsync(string productId)
        {
            var productIngredients = this._productsIngredientsRepository
                .All()
                .Where(pi => pi.ProductId == productId)
                .ToList();

            if (productIngredients.Any())
            {
                this._productsIngredientsRepository.DeleteRange(productIngredients);
                await this._productsIngredientsRepository.SaveChangesAsync();
            } 
        }
    }
}
