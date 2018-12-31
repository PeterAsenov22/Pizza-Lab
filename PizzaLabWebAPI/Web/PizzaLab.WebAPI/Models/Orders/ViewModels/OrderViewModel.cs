namespace PizzaLab.WebAPI.Models.Orders.ViewModels
{
    using System;
    using System.Collections.Generic;

    public class OrderViewModel
    {
        public string Id { get; set; }

        public string CreatorId { get; set; }

        public string CreatorEmail { get; set; }

        public DateTime DateCreated { get; set; }

        public string Status { get; set; }

        public IEnumerable<OrderProductViewModel> OrderProducts { get; set; }
    }
}
