namespace PizzaLab.Services.DataServices.Models.Orders
{
    public class OrderProductDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
