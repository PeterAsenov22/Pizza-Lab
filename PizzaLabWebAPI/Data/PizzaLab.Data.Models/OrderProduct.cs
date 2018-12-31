namespace PizzaLab.Data.Models
{
    using Common;

    public class OrderProduct : BaseModel<int>
    {
        public string ProductId { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
