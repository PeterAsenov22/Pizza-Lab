namespace PizzaLab.Services.DataServices.Contracts
{
    using Models.Categories;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoriesService
    {
        IEnumerable<CategoryDto> All();

        bool Any();

        Task CreateAsync(string categoryName);

        Task CreateRangeAsync(string[] categoriesName);

        CategoryDto FindByName(string categoryName);
    }
}
