namespace PizzaLab.WebAPI.Areas.Admin.Models.Ingredients.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class IngredientInputModel
    {
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Name should be at least 3 characters long and not more than 20.")]
        public string Name { get; set; }
    }
}
