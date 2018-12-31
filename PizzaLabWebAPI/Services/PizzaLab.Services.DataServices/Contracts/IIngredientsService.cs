namespace PizzaLab.Services.DataServices.Contracts
{
    using Models.Ingredients;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IIngredientsService
    {
        bool Any();

        IEnumerable<IngredientDto> All();

        Task CreateAsync(string ingredientName);

        Task CreateRangeAsync(string[] ingredientsName);

        IngredientDto FindByName(string ingredientName);
    }
}
