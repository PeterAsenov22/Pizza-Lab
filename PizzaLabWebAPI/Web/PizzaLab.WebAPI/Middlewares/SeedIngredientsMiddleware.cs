namespace PizzaLab.WebAPI.Middlewares
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Services.DataServices.Contracts;
    using System;
    using System.Threading.Tasks; 
    
    public class SeedIngredientsMiddleware
    {
        private readonly RequestDelegate _next;

        public SeedIngredientsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider provider)
        {
            var ingredientsService = provider.GetService<IIngredientsService>();
            if (!ingredientsService.Any())
            {
                await ingredientsService.CreateRangeAsync(new string[]
                {
                    "olive oil",
                    "oregano",
                    "pepperoni salami",
                    "yellow cheese",
                    "tomato sauce",
                    "ham",
                    "mushrooms",
                    "smoked cheese",
                    "traditional bulgarian flat sausage called lukanka",
                    "chicken roll",
                    "corn",
                    "red peppers",
                    "hot peppers",
                    "chicken",
                    "avocado",
                    "olives",
                    "pineapple",
                    "white bulgarian cheese",
                    "blue cheese",
                    "philadelphia",
                    "tuna fish",
                    "white pepper",
                    "cherry tomatoes",
                    "basil chips",
                    "chorizo",
                    "proschuitto",
                    "eggs",
                    "bacon",
                    "red onion",
                    "mozzarella",
                    "parsley"
                });
            }

            await this._next(context);
        }
    }
}
