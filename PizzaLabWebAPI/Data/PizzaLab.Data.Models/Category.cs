namespace PizzaLab.Data.Models
{
    using Common;
    using System.Collections.Generic;

    public class Category : BaseModel<int>
    {
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
