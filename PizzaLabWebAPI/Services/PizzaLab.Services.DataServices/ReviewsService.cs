namespace PizzaLab.Services.DataServices
{
    using Contracts;
    using Data.Common;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ReviewsService : IReviewsService
    {
        private readonly IRepository<Review> _reviewsRepository;

        public ReviewsService(IRepository<Review> reviewsRepository)
        {
            this._reviewsRepository = reviewsRepository;
        }

        public async Task<Review> CreateAsync(string text, string creatorId, string productId)
        {
            var review = new Review
            {
                Text = text,
                CreatorId = creatorId,
                ProductId = productId,
                LastModified = DateTime.Now
            };

            await this._reviewsRepository.AddAsync(review);
            await this._reviewsRepository.SaveChangesAsync();

            return review;
        }

        public IEnumerable<Review> GetProductReviews(string productId)
        {
            return this._reviewsRepository
                .All()
                .Include(r => r.Creator)
                .Where(r => r.ProductId == productId)
                .ToList();
        }

        public async Task DeleteProductReviewsAsync(string productId)
        {
            var reviews = this._reviewsRepository
                .All()
                .Where(r => r.ProductId == productId)
                .ToList();

            if (reviews.Any())
            {
                this._reviewsRepository.DeleteRange(reviews);
                await this._reviewsRepository.SaveChangesAsync();
            }          
        }
    }
}
