namespace PizzaLab.Services.DataServices.Contracts
{
    using Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IIngredientsService
    {
        bool Any();

        IEnumerable<Ingredient> All();

        Task CreateAsync(string ingredientName);

        Task CreateRangeAsync(string[] ingredientsName);
    }
}
