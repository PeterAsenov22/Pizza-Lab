namespace PizzaLab.Services.DataServices.Contracts
{
    using Data.Models;
    using System.Threading.Tasks;

    public interface ICategoriesService
    {
        bool Any();

        Task CreateAsync(string categoryName);

        Task CreateRangeAsync(string[] categoriesName);

        Category FindByName(string categoryName);
    }
}
