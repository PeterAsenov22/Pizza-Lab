namespace PizzaLab.Services.DataServices.Models.Products
{
    using Categories;
    using Data.Models;
    using Ingredients;
    using System.Collections.Generic;

    public class ProductDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Weight { get; set; }

        public string Image { get; set; }

        public int CategoryId { get; set; }

        public CategoryDto Category { get; set; }

        public IEnumerable<IngredientDto> Ingredients { get; set; }

        public IEnumerable<ApplicationUser> Likes { get; set; }
    }
}
