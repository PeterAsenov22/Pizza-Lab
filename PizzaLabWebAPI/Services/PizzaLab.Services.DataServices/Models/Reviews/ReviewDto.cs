namespace PizzaLab.Services.DataServices.Models.Reviews
{
    using System;

    public class ReviewDto
    {
        public string Id { get; set; }

        public string ReviewText { get; set; }

        public string CreatorUsername { get; set; }

        public DateTime LastModified { get; set; }
    }
}
