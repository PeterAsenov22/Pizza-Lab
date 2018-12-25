namespace PizzaLab.Services.DataServices.Contracts
{
    using Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IReviewsService
    {
        Task<Review> CreateAsync(string text, string creatorId, string productId);

        IEnumerable<Review> GetProductReviews(string productId);
    }
}
