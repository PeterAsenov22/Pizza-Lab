namespace PizzaLab.WebAPI.Models.Reviews.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class CreateReviewInputModel
    {
        private const int ReviewMinimumLength = 4;
        private const string ReviewErrorMessage = "Review Text should be at least 4 characters long.";

        [Required]
        [MinLength(ReviewMinimumLength, ErrorMessage = ReviewErrorMessage)]
        public string Review { get; set; }
    }
}
