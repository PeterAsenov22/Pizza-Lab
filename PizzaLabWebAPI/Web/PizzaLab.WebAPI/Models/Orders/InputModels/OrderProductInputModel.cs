namespace PizzaLab.WebAPI.Models.Orders.InputModels
{
    public class OrderProductInputModel
    {
        public string Id { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
