namespace PizzaLab.Services.DataServices.Models.Orders
{
    using System;
    using System.Collections.Generic;

    public class OrderDto
    {
        public string Id { get; set; }

        public string CreatorId { get; set; }

        public string CreatorEmail { get; set; }

        public DateTime CreationDate { get; set; }

        public string Status { get; set; }

        public IEnumerable<OrderProductDto> OrderProducts { get; set; }
    }
}
