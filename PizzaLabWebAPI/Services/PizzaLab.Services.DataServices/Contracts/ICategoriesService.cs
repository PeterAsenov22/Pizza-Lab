namespace PizzaLab.Services.DataServices.Contracts
{
    using System.Threading.Tasks;

    public interface ICategoriesService
    {
        bool Any();

        Task CreateAsync(string categoryName);

        Task CreateRangeAsync(string[] categoriesName);
    }
}
