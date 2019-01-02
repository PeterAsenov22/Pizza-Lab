namespace PizzaLab.WebAPI.Controllers
{
    using AutoMapper;
    using Data.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Common;
    using Models.Reviews.InputModels;
    using Models.Reviews.ViewModels;
    using Services.DataServices.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ReviewsController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly IProductsService _productsService;
        private readonly IReviewsService _reviewsService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReviewsController(
            IMapper mapper,
            IProductsService productsService,
            IReviewsService reviewsService,
            UserManager<ApplicationUser> userManager)
        {
            this._mapper = mapper;
            this._productsService = productsService;
            this._reviewsService = reviewsService;
            this._userManager = userManager;
        }

        [HttpGet("{productId}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<IEnumerable<ReviewViewModel>> Get(string productId)
        {
            if (!this._productsService.Exists(productId))
            {
                return BadRequest(new BadRequestViewModel
                {
                    Message = "Product not found."
                });
            }

            return this._reviewsService
                .GetProductReviews(productId)
                .Select(r => this._mapper.Map<ReviewViewModel>(r))
                .ToList();
        }

        [HttpPost("{productId}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<SuccessViewModel<ReviewViewModel>>> Post([FromRoute] string productId, [FromBody] CreateReviewInputModel model)
        {
            if (!this._productsService.Exists(productId))
            {
                return BadRequest(new BadRequestViewModel
                {
                    Message = "Product not found."
                });
            }

            try
            {
                var creator = await this._userManager.FindByNameAsync(this.User.Identity.Name);
                var review = await this._reviewsService.CreateAsync(model.Review, creator.Id, productId);

                return new SuccessViewModel<ReviewViewModel>
                {
                    Message = "Review added successfully.",
                    Data = this._mapper.Map<ReviewViewModel>(review)
                };
            }
            catch (Exception)
            {
                return BadRequest(new BadRequestViewModel
                {
                    Message = "Something went wrong."
                });
            }
        }

        [HttpDelete("{reviewId}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Delete(string reviewId)
        {
            if (!this._reviewsService.Exists(reviewId))
            {
                return BadRequest(new BadRequestViewModel
                {
                    Message = $"Review with id {reviewId} was not found."
                });
            }

            var reviewCreatorName = this._reviewsService.FindReviewCreatorById(reviewId);
            if (!this.User.IsInRole("Administrator") && this.User.Identity.Name != reviewCreatorName)
            {
                return Unauthorized();
            }

            try
            {
                await this._reviewsService.DeleteReviewAsync(reviewId);

                return Ok(new
                {
                    Message = "Review deleted successfully."
                });
            }
            catch (Exception)
            {
                return BadRequest(new BadRequestViewModel
                {
                    Message = "Something went wrong."
                });
            }          
        }
    }
}
