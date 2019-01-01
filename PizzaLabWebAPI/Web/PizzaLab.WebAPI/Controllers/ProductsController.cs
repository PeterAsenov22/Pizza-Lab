namespace PizzaLab.WebAPI.Controllers
{
    using AutoMapper;
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Common;
    using Models.Products.ViewModels;
    using Services.DataServices.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("api/[controller]/[action]")]
    public class ProductsController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly IProductsService _productsService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUsersLikesService _usersLikesService;

        public ProductsController(
            IMapper mapper,
            IProductsService productsService,
            UserManager<ApplicationUser> userManager,
            IUsersLikesService usersLikesService)
        {
            this._mapper = mapper;
            this._productsService = productsService;
            this._userManager = userManager;
            this._usersLikesService = usersLikesService;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ProductViewModel>> All()
        {
            return this._productsService
                .All()
                .Select(p => this._mapper.Map<ProductViewModel>(p))
                .ToList();
        }

        [HttpPost("{productId}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Like(string productId)
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
                var user = await this._userManager.FindByNameAsync(this.User.Identity.Name);
                var product = this._productsService.All().First(p => p.Id == productId);

                if (product.Likes.Any(u => u.Id == user.Id))
                {
                    return BadRequest(new BadRequestViewModel
                    {
                        Message = "You have already liked this product."
                    });
                }

                await this._usersLikesService.CreateUserLike(productId, user.Id);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest(new BadRequestViewModel
                {
                    Message = "Something went wrong."
                });
            }
        }

        [HttpPost("{productId}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Unlike(string productId)
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
                var user = await this._userManager.FindByNameAsync(this.User.Identity.Name);
                await this._usersLikesService.DeleteUserLikeAsync(productId, user.Id);

                return Ok();
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
