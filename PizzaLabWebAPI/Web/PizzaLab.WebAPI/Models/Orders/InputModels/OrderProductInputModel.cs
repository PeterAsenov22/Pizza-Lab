namespace PizzaLab.WebAPI.Models.Orders.InputModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class OrderProductInputModel
    {
        private const double MinimumPrice = 0.1;
        private const int MinimumQuantity = 1;

        private const string PriceErrorMessage = "Price should be a positive number.";
        private const string QuantityErrorMessage = "Quantity should be a positive number.";

        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(MinimumPrice, Double.MaxValue, ErrorMessage = PriceErrorMessage)]
        public decimal Price { get; set; }

        [Range(MinimumQuantity, int.MaxValue, ErrorMessage = QuantityErrorMessage)]
        public int Quantity { get; set; }
    }
}
