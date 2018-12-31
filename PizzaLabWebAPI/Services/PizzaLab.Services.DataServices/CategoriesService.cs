namespace PizzaLab.Services.DataServices
{
    using AutoMapper;
    using Contracts;
    using Data.Common;
    using Data.Models;
    using Models.Categories;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CategoriesService : ICategoriesService
    {
        private readonly IRepository<Category> _categoriesRepository;
        private readonly IMapper _mapper;

        public CategoriesService(
            IRepository<Category> categoriesRepository,
            IMapper mapper)
        {
            this._categoriesRepository = categoriesRepository;
            this._mapper = mapper;
        }

        public IEnumerable<CategoryDto> All()
        {
            return this._categoriesRepository
                .All()
                .Select(c => this._mapper.Map<CategoryDto>(c))
                .OrderBy(c => c.Name);
        }

        public bool Any()
        {
            return this._categoriesRepository.All().Any();
        }

        public async Task CreateAsync(string categoryName)
        {
            var category = new Category
            {
                Name = categoryName
            };

            await this._categoriesRepository.AddAsync(category);
            await this._categoriesRepository.SaveChangesAsync();
        }

        public async Task CreateRangeAsync(string[] categoriesName)
        {
            var categories = categoriesName
                .Select(categoryName => new Category
            {
                Name = categoryName
            });

            await this._categoriesRepository.AddRangeAsync(categories);
            await this._categoriesRepository.SaveChangesAsync();
        }

        public CategoryDto FindByName(string categoryName)
        {
            return this._categoriesRepository
                .All()
                .Select(c => this._mapper.Map<CategoryDto>(c))
                .FirstOrDefault(c => c.Name == categoryName);
        }
    }
}
