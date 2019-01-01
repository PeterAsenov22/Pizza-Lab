namespace PizzaLab.WebAPI.Controllers
{
    using AutoMapper;   
    using Data.Models;
    using Models.Common;
    using Models.Orders.InputModels;
    using Models.Orders.ViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.DataServices.Contracts;
    using Services.DataServices.Models.Orders;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("api/[controller]/[action]")]
    public class OrdersController : ApiController
    {
        private readonly IProductsService _productsService;
        private readonly IOrdersService _ordersService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public OrdersController(
            IProductsService productsService,
            IOrdersService ordersService,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            this._productsService = productsService;
            this._ordersService = ordersService;
            this._userManager = userManager;
            this._mapper = mapper;
        }

        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<SuccessViewModel<OrderViewModel>>> Submit([FromBody] OrderInputModel model)
        {
            foreach (var product in model.OrderProducts)
            {
                if (!this._productsService.Exists(product.Id))
                {
                    return BadRequest(new BadRequestViewModel
                    {
                        Message = $"Product with id {product.Id} not found."
                    });
                }
            }

            try
            {
                var user = await this._userManager.FindByNameAsync(this.User.Identity.Name);
                var orderProducts = model.OrderProducts
                    .Select(op => this._mapper.Map<OrderProductDto>(op))
                    .ToList();

                var orderDto = await this._ordersService.CreateOrderAsync(user.Id, orderProducts);

                return new SuccessViewModel<OrderViewModel>
                {
                    Data = this._mapper.Map<OrderViewModel>(orderDto),
                    Message = "Your order was processed successfully."
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

        [HttpGet]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<OrderViewModel>>> My()
        {
            try
            {
                var user = await this._userManager.FindByNameAsync(this.User.Identity.Name);
                return this._ordersService
                    .GetUserOrders(user.Id)
                    .Select(orderDto => this._mapper.Map<OrderViewModel>(orderDto))
                    .ToList();
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
