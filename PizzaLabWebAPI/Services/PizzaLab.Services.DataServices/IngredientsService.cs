namespace PizzaLab.Services.DataServices
{
    using AutoMapper;
    using Contracts;
    using Data.Common;
    using Data.Models;
    using Models.Ingredients;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class IngredientsService : IIngredientsService
    {
        private readonly IRepository<Ingredient> _ingredientsRepository;
        private readonly IMapper _mapper;

        public IngredientsService(
            IRepository<Ingredient> ingredientsRepository,
            IMapper mapper)
        {
            this._ingredientsRepository = ingredientsRepository;
            this._mapper = mapper;
        }

        public bool Any()
        {
            return this._ingredientsRepository.All().Any();
        }

        public IEnumerable<IngredientDto> All()
        {
            return this._ingredientsRepository
                .All()
                .Select(i => this._mapper.Map<IngredientDto>(i))
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

        public IngredientDto FindByName(string ingredientName)
        {
            return this._ingredientsRepository
                .All()
                .Select(i => this._mapper.Map<IngredientDto>(i))
                .FirstOrDefault(i => i.Name.ToLower() == ingredientName.ToLower());
        }
    }
}
