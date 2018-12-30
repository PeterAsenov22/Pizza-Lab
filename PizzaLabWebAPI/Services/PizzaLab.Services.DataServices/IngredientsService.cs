namespace PizzaLab.Services.DataServices
{
    using Contracts;
    using Data.Common;
    using Data.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class IngredientsService : IIngredientsService
    {
        private readonly IRepository<Ingredient> _ingredientsRepository;

        public IngredientsService(IRepository<Ingredient> ingredientsRepository)
        {
            this._ingredientsRepository = ingredientsRepository;
        }

        public bool Any()
        {
            return this._ingredientsRepository.All().Any();
        }

        public IEnumerable<Ingredient> All()
        {
            return this._ingredientsRepository
                .All()
                .OrderBy(i => i.Name);
        }

        public async Task CreateAsync(string ingredientName)
        {
            var ingredient = new Ingredient
            {
                Name = ingredientName
            };

            await this._ingredientsRepository.AddAsync(ingredient);
            await this._ingredientsRepository.SaveChangesAsync();
        }

        public async Task CreateRangeAsync(string[] ingredientsName)
        {
            var ingredients = ingredientsName.Select(ingredientName => new Ingredient
            {
                Name = ingredientName
            });

            await this._ingredientsRepository.AddRangeAsync(ingredients);
            await this._ingredientsRepository.SaveChangesAsync();
        }

        public Ingredient FindByName(string ingredientName)
        {
            return this._ingredientsRepository.All().FirstOrDefault(i => i.Name.ToLower() == ingredientName.ToLower());
        }
    }
}
