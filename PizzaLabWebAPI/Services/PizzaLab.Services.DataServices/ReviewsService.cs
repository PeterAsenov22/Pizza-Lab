namespace PizzaLab.Services.DataServices
{
    using AutoMapper;
    using Contracts;
    using Data.Common;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models.Reviews;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ReviewsService : IReviewsService
    {
        private readonly IRepository<Review> _reviewsRepository;
        private readonly IMapper _mapper;

        public ReviewsService(
            IRepository<Review> reviewsRepository,
            IMapper mapper)
        {
            this._reviewsRepository = reviewsRepository;
            this._mapper = mapper;
        }

        public async Task<ReviewDto> CreateAsync(string text, string creatorId, string productId)
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

            return this._mapper.Map<ReviewDto>(review);
        }

        public IEnumerable<ReviewDto> GetProductReviews(string productId)
        {
            return this._reviewsRepository
                .All()
                .Include(r => r.Creator)
                .Where(r => r.ProductId == productId)
                .Select(r => this._mapper.Map<ReviewDto>(r))               
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

        public async Task DeleteReviewAsync(string reviewId)
        {
            var review = this._reviewsRepository
                .All()
                .First(r => r.Id == reviewId);

            this._reviewsRepository.Delete(review);
            await this._reviewsRepository.SaveChangesAsync();
        }

        public bool Exists(string reviewId)
        {
            return this._reviewsRepository
                .All()
                .Any(r => r.Id == reviewId);
        }

        public string FindReviewCreatorById(string reviewId)
        {
            return this._reviewsRepository
                .All()
                .Include(r => r.Creator)
                .First(r => r.Id == reviewId)
                .Creator
                .UserName;
        }
    }
}
