namespace PizzaLab.WebAPI.Areas.Admin.Models.Products.InputModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ProductInputModel
    {
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Name should be at least 3 characters long and not more than 20.")]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }

        public List<string> Ingredients { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 10, ErrorMessage = "Description should be at least 10 characters long and not more than 120.")]
        public string Description { get; set; }

        [Required]
        [MinLength(14, ErrorMessage = "Image URL should be at least 14 characters long.")]
        [RegularExpression(@"^(http:|https:)\/\/.+", ErrorMessage = "Image URL should be valid.")]
        public string Image { get; set; }

        [Range(250, 800, ErrorMessage = "Weight should be between 250 and 800 grams.")]
        public int Weight { get; set; }

        [Range(0.1, Double.MaxValue, ErrorMessage = "Price should be a positive number.")]
        public decimal Price { get; set; }
    }
}
