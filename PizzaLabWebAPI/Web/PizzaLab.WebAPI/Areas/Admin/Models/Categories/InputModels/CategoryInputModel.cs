namespace PizzaLab.WebAPI.Areas.Admin.Models.Categories.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class CategoryInputModel
    {
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Name should be at least 3 characters long and not more than 20.")]
        public string Name { get; set; }
    }
}
