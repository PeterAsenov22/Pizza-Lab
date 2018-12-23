namespace PizzaLab.Data.Models
{
    using Common;
    using System.Collections.Generic;

    public class Ingredient : BaseModel<int>
    {
        public string Name { get; set; }

        public ICollection<ProductsIngredients> Products { get; set; }
    }
}
