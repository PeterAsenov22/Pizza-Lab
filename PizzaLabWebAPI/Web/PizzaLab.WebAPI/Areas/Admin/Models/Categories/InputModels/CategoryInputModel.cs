namespace PizzaLab.WebAPI.Areas.Admin.Models.Categories.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class CategoryInputModel
    {
        private const int NameMinimumLength = 3;
        private const int NameMaximumLength = 20;
        private const string NameErrorMessage = "Name should be at least 3 characters long and not more than 20.";

        [Required]
        [StringLength(NameMaximumLength, MinimumLength = NameMinimumLength, ErrorMessage = NameErrorMessage)]
        public string Name { get; set; }
    }
}
