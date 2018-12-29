namespace PizzaLab.Services.DataServices.Contracts
{
    using Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoriesService
    {
        IEnumerable<Category> All();

        bool Any();

        Task CreateAsync(string categoryName);

        Task CreateRangeAsync(string[] categoriesName);

        Category FindByName(string categoryName);
    }
}
