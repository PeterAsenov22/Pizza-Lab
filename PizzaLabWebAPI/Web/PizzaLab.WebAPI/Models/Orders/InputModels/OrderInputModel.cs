namespace PizzaLab.WebAPI.Models.Orders.InputModels
{
    using System.Collections.Generic;

    public class OrderInputModel
    {
        public IEnumerable<OrderProductInputModel> OrderProducts { get; set; }
    }
}
