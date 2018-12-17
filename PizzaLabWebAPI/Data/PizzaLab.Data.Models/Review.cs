namespace PizzaLab.Data.Models
{
    using Common;
    using System;   

    public class Review : BaseModel<string>
    {
        public string Text { get; set; }

        public string CreatorId { get; set; }

        public ApplicationUser Creator { get; set; }

        public string ProductId { get; set; }

        public Product Product { get; set; }

        public DateTime LastModified { get; set; }
    }
}
