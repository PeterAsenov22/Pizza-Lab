namespace PizzaLab.Services.DataServices
{
    using Contracts;
    using Data.Common;
    using Data.Models;
    using System.Linq;
    using System.Threading.Tasks;

    public class CategoriesService : ICategoriesService
    {
        private readonly IRepository<Category> _categoriesRepository;

        public CategoriesService(IRepository<Category> categoriesRepository)
        {
            this._categoriesRepository = categoriesRepository;
        }

        public bool Any()
        {
            return this._categoriesRepository.All().Any();
        }

        public async Task CreateAsync(string categoryName)
        {
            var category = new Category()
            {
                Name = categoryName
            };

            await this._categoriesRepository.AddAsync(category);
            await this._categoriesRepository.SaveChangesAsync();
        }

        public async Task CreateRangeAsync(string[] categoriesName)
        {
            var categories = categoriesName.Select(categoryName => new Category()
            {
                Name = categoryName
            });

            await this._categoriesRepository.AddRangeAsync(categories);
            await this._categoriesRepository.SaveChangesAsync();
        }
    }
}
