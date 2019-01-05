namespace PizzaLab.WebAPI.Areas.Admin.Models.Products.InputModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ProductInputModel
    {
        private const int NameMinimumLength = 3;
        private const int NameMaximumLength = 20;
        private const int DescriptionMinimumLength = 10;
        private const int DescriptionMaximumLength = 220;
        private const int ImageMinimumLength = 14;
        private const int MinimumWeight = 250;
        private const int MaximumWeight = 800;
        private const double MinimumPrice = 0.1;
        
        private const string NameErrorMessage = "Name should be at least 3 characters long and not more than 20.";
        private const string DescriptionErrorMessage = "Description should be at least 10 characters long and not more than 220.";
        private const string ImageErrorMessage = "Image URL should be at least 14 characters long.";
        private const string ImageRegex = @"^(http:|https:)\/\/.+";
        private const string ImageRegexErrorMessage = "Image URL should be valid.";
        private const string WeightErrorMessage = "Weight should be between 250 and 800 grams";
        private const string PriceErrorMessage = "Price should be a positive number.";

        [Required]
        [StringLength(NameMaximumLength, MinimumLength = NameMinimumLength, ErrorMessage = NameErrorMessage)]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }

        public List<string> Ingredients { get; set; }

        [Required]
        [StringLength(DescriptionMaximumLength, MinimumLength = DescriptionMinimumLength, ErrorMessage = DescriptionErrorMessage)]
        public string Description { get; set; }

        [Required]
        [MinLength(ImageMinimumLength, ErrorMessage = ImageErrorMessage)]
        [RegularExpression(ImageRegex, ErrorMessage = ImageRegexErrorMessage)]
        public string Image { get; set; }

        [Range(MinimumWeight, MaximumWeight, ErrorMessage = WeightErrorMessage)]
        public int Weight { get; set; }

        [Range(MinimumPrice, Double.MaxValue, ErrorMessage = PriceErrorMessage)]
        public decimal Price { get; set; }
    }
}
