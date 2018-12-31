namespace PizzaLab.WebAPI.Models.Orders.ViewModels
{
    public class OrderProductViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
