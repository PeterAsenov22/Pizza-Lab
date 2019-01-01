namespace PizzaLab.WebAPI.Middlewares
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Services.DataServices.Contracts;
    using Services.DataServices.Models.Ingredients;
    using Services.DataServices.Models.Products;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SeedProductsMiddleware
    {
        private readonly RequestDelegate _next;

        public SeedProductsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider provider)
        {
            var productsService = provider.GetService<IProductsService>();
            
            if (!productsService.Any())
            {
                var ingredientsService = provider.GetService<IIngredientsService>();
                var categoriesService = provider.GetService<ICategoriesService>();
                var products = new List<ProductDto>();

                var margheritaIngredients = new string[] { "olive oil", "yellow cheese", "tomato sauce" };
                var margherita = new ProductDto
                {
                    Name = "Margherita",
                    Description = "Pizza Margherita is a typical Neapolitan pizza, made with San Marzano tomatoes, mozzarella fior di latte, fresh basil, salt and extra-virgin olive oil.",
                    Price = 5.90m,
                    Weight = 350,
                    Image = "https://d2gg9evh47fn9z.cloudfront.net/800px_COLOURBOX3469401.jpg",
                    CategoryId = categoriesService.FindByName("Vegetarian").Id,
                    Ingredients = ingredientsService
                        .All()
                        .Where(i => margheritaIngredients.Contains(i.Name))
                        .Select(i => new IngredientDto { Id = i.Id, Name = i.Name})
                        .ToList()
                };
                products.Add(margherita);

                var pepperoniIngredients = new string[] { "olive oil", "pepperoni salami", "yellow cheese", "tomato sauce", "oregano" };
                var pepperoni = new ProductDto
                {
                    Name = "Pepperoni",
                    Description = "Pepperoni is an American variety of salami, made from cured pork and beef mixed together and seasoned with paprika or other chili pepper.",
                    Price = 9.90m,
                    Weight = 500,
                    Image = "https://pizzamuensingen.ch/wp-content/uploads/2017/10/pizza_adven_zestypepperoni.png",
                    CategoryId = categoriesService.FindByName("American").Id,
                    Ingredients = ingredientsService
                        .All()
                        .Where(i => pepperoniIngredients.Contains(i.Name))
                        .Select(i => new IngredientDto { Id = i.Id, Name = i.Name})
                        .ToList()
                };
                products.Add(pepperoni);

                var calzoneIngredients = new string[] { "ham", "traditional bulgarian flat sausage called lukanka",
                    "mushrooms", "yellow cheese", "smoked cheese", "mozzarella", "tomato sauce" };
                var calzone = new ProductDto
                {
                    Name = "Calzone",
                    Description = "A calzone is an Italian oven-baked folded pizza that originated in Naples. A typical calzone is made from salted bread dough, stuffed with salami, ham, vegetables, mozzarella, Parmesan and an egg.",
                    Price = 11.90m,
                    Weight = 500,
                    Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRGOxB3Fe3lAvkYQBdWrzp8885FC2uAH5nlZo-ZO21TkmxO5wa_",
                    CategoryId = categoriesService.FindByName("Italian").Id,
                    Ingredients = ingredientsService
                        .All()
                        .Where(i => calzoneIngredients.Contains(i.Name))
                        .Select(i => new IngredientDto { Id = i.Id, Name = i.Name })
                        .ToList()
                };
                products.Add(calzone);

                var polloIngredients = new string[] { "chicken roll", "corn",
                    "red peppers", "yellow cheese", "smoked cheese", "tomato sauce" };
                var pollo = new ProductDto
                {
                    Name = "Pollo",
                    Description = "Pollo might be your choice when you are in the mood for something healthy. Tender grilled chicken, creamy feta, roasted red peppers and corn are generously piled on top of our famous tomato sauce.",
                    Price = 10.90m,
                    Weight = 550,
                    Image = "http://www.ilforno.bg/45-large_default/polo.jpg",
                    CategoryId = categoriesService.FindByName("Italian").Id,
                    Ingredients = ingredientsService
                        .All()
                        .Where(i => polloIngredients.Contains(i.Name))
                        .Select(i => new IngredientDto { Id = i.Id, Name = i.Name })
                        .ToList()
                };
                products.Add(pollo);

                var diabloIngredients = new string[] { "ham", "mushrooms",
                    "hot peppers", "yellow cheese", "tomato sauce", "olive oil" };
                var diablo = new ProductDto
                {
                    Name = "Diablo",
                    Description = "Pizza diavola means the devils pizza and is quite a spicy little devil and one of my favourite pizzas. If you like spicy hot and chilli flavours you will enjoy this pizza.",
                    Price = 8.90m,
                    Weight = 500,
                    Image = "https://images.pizza33.ua/products/product/yQfkJqZweoLn9omo68oz5BnaGzaIE0UJ.jpg",
                    CategoryId = categoriesService.FindByName("Premium").Id,
                    Ingredients = ingredientsService
                        .All()
                        .Where(i => diabloIngredients.Contains(i.Name))
                        .Select(i => new IngredientDto { Id = i.Id, Name = i.Name })
                        .ToList()
                };
                products.Add(diablo);

                var californiaIngredients = new string[] { "chicken", "avocado",
                    "olives", "yellow cheese", "pineapple", "tomato sauce" };
                var california = new ProductDto
                {
                    Name = "California",
                    Description = "California pizza is a style of single-serving pizza that combines New York and Italian thin crust with toppings from the California cuisine cooking style.",
                    Price = 13.90m,
                    Weight = 400,
                    Image = "https://s3-us-west-2.amazonaws.com/craftcms-pizzaranch/general-uploads/Menu-Images/_960x800_crop_center-center/California_Chicken.png?mtime=20171002081409",
                    CategoryId = categoriesService.FindByName("American").Id,
                    Ingredients = ingredientsService
                        .All()
                        .Where(i => californiaIngredients.Contains(i.Name))
                        .Select(i => new IngredientDto { Id = i.Id, Name = i.Name })
                        .ToList()
                };
                products.Add(california);

                var fourCheesesIngredients = new string[] { "white bulgarian cheese", "blue cheese",
                    "smoked cheese", "yellow cheese", "tomato sauce" };
                var fourCheeses = new ProductDto
                {
                    Name = "Four Cheeses",
                    Description = "Pizza cheese encompasses several varieties and types of cheeses and dairy products. These include processed and modified cheese such as mozzarella-like processed cheeses and mozzarella variants.",
                    Price = 9.80m,
                    Weight = 500,
                    Image = "https://thumbs.dreamstime.com/b/four-cheese-pizza-mozzarella-cheese-dorblu-cheddar-cheese-parmesan-cheese-isolated-white-background-91847479.jpg",
                    CategoryId = categoriesService.FindByName("Vegetarian").Id,
                    Ingredients = ingredientsService
                        .All()
                        .Where(i => fourCheesesIngredients.Contains(i.Name))
                        .Select(i => new IngredientDto { Id = i.Id, Name = i.Name })
                        .ToList()
                };
                products.Add(fourCheeses);

                var tunaIngredients = new string[] { "philadelphia", "tuna fish",
                    "white pepper", "yellow cheese", "cherry tomatoes", "basil chips", "olives" };
                var tuna = new ProductDto
                {
                    Name = "Tuna",
                    Description = "If you like tuna you should try this tuna and red onion pizza. Thin crust with tuna, red onion flavor, black olives and fresh basil leaves makes it one delightful meal.",
                    Price = 16.70m,
                    Weight = 420,
                    Image = "https://www.pizzaexpress.com.my/wp-content/uploads/2017/06/tuna.png",
                    CategoryId = categoriesService.FindByName("Premium").Id,
                    Ingredients = ingredientsService
                        .All()
                        .Where(i => tunaIngredients.Contains(i.Name))
                        .Select(i => new IngredientDto { Id = i.Id, Name = i.Name })
                        .ToList()
                };
                products.Add(tuna);

                var quattroStagioniIngredients = new string[] { "tomato sauce", "chorizo",
                    "proschuitto", "chicken roll", "bacon", "corn", "eggs", "yellow cheese", "red onion" };
                var quattroStagioni = new ProductDto
                {
                    Name = "Quattro Stagioni",
                    Description = "Pizza quattro stagioni is a variety of pizza in Italian cuisine that is prepared in four sections with diverse ingredients, with each section representing one season of the year.",
                    Price = 13.10m,
                    Weight = 500,
                    Image = "http://www.svila.it/php/components/com_virtuemart/shop_image/product/Pizza_Quattro_st_4d1c68920ee08.jpg",
                    CategoryId = categoriesService.FindByName("Italian").Id,
                    Ingredients = ingredientsService
                        .All()
                        .Where(i => quattroStagioniIngredients.Contains(i.Name))
                        .Select(i => new IngredientDto { Id = i.Id, Name = i.Name })
                        .ToList()
                };
                products.Add(quattroStagioni);

                var ratatouilleIngredients = new string[] { "tomato sauce", "red onion", "red peppers", "olives", "yellow cheese" };
                var ratatouille = new ProductDto
                {
                    Name = "Ratatouille",
                    Description = "The ratatouille pizza is packed full of summer vegetables. We piled them high on a whole wheat pizza crust and sprinkled it all with cheese.",
                    Price = 9.90m,
                    Weight = 420,
                    Image = "https://previews.123rf.com/images/eivaisla/eivaisla1611/eivaisla161100003/66411516-delicious-vegetarian-pizza-isolated-on-white-background-high-angle-shot-.jpg",
                    CategoryId = categoriesService.FindByName("Premium").Id,
                    Ingredients = ingredientsService
                        .All()
                        .Where(i => ratatouilleIngredients.Contains(i.Name))
                        .Select(i => new IngredientDto { Id = i.Id, Name = i.Name })
                        .ToList()
                };
                products.Add(ratatouille);

                var doubleEggsIngredients = new string[] { "tomato sauce", "red onion", "red peppers", "olives", "yellow cheese" };
                var doubleEggs = new ProductDto
                {
                    Name = "Double Eggs",
                    Description = "Switch it up! Have pizza at breakfast. Ripe tomatoes, fresh parsley, ham, and double eggs make for a filling start to your day.",
                    Price = 9.90m,
                    Weight = 500,
                    Image = "https://us.123rf.com/450wm/deyanarobova/deyanarobova1609/deyanarobova160900037/69702341-pizza-on-a-white-background-with-eggs-ham-cheese-and-peppers-.jpg?ver=6",
                    CategoryId = categoriesService.FindByName("Traditional").Id,
                    Ingredients = ingredientsService
                        .All()
                        .Where(i => doubleEggsIngredients.Contains(i.Name))
                        .Select(i => new IngredientDto { Id = i.Id, Name = i.Name })
                        .ToList()
                };
                products.Add(doubleEggs);

                await productsService.CreateRangeAsync(products);
            }

            await this._next(context);
        }
    }
}
