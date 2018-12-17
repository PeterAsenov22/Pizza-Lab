namespace PizzaLab.Data.Models
{
    using Common;

    public class OrderProduct : BaseModel<int>
    {
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
