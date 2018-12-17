namespace PizzaLab.Data.Models
{
    using Common;

    public class Ingredient : BaseModel<int>
    {
        public string Name { get; set; }
    }
}
