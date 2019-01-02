namespace PizzaLab.Services.DataServices.Contracts
{
    using Models.Reviews;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IReviewsService
    {
        Task<ReviewDto> CreateAsync(string text, string creatorId, string productId);

        IEnumerable<ReviewDto> GetProductReviews(string productId);

        Task DeleteProductReviewsAsync(string productId);

        Task DeleteReviewAsync(string reviewId);

        bool Exists(string reviewId);

        string FindReviewCreatorById(string reviewId);
    }
}
