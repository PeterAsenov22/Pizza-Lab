namespace PizzaLab.Data.Models
{
    using Common;
    using Enums;
    using System;
    using System.Collections.Generic; 

    public class Order : BaseModel<string>
    {
        public string CreatorId { get; set; }

        public ApplicationUser Creator { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime CreationDate { get; set; }

        public ICollection<OrderProduct> Products { get; set; }
    }
}
