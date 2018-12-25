namespace PizzaLab.WebAPI.Models.Reviews.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class CreateReviewInputModel
    {
        [Required]
        [MinLength(4, ErrorMessage = "Review Text should be at least 4 characters long.")]
        public string Review { get; set; }
    }
}
