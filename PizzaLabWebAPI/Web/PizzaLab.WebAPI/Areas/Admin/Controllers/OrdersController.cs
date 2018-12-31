namespace PizzaLab.WebAPI.Areas.Admin.Controllers
{  
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Services.DataServices.Contracts;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WebAPI.Controllers;
    using WebAPI.Models.Common;
    using WebAPI.Models.Orders.ViewModels;
    
    [Route("api/admin/[controller]/[action]")]
    public class OrdersController : ApiController
    {
        private readonly IOrdersService _ordersService;
        private readonly IMapper _mapper;

        public OrdersController(
            IOrdersService ordersService,
            IMapper mapper)
        {
            this._ordersService = ordersService;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<IEnumerable<OrderViewModel>> Pending()
        {
            if (this.User.IsInRole("Administrator"))
            {
                return this._ordersService
                    .GetPendingOrders()
                    .Select(orderDto => this._mapper.Map<OrderViewModel>(orderDto))
                    .ToList();
            }

            return Unauthorized();
        }

        [HttpGet]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<IEnumerable<OrderViewModel>> Approved()
        {
            if (this.User.IsInRole("Administrator"))
            {
                return this._ordersService
                    .GetApprovedOrders()
                    .Select(orderDto => this._mapper.Map<OrderViewModel>(orderDto))
                    .ToList();
            }

            return Unauthorized();
        }

        [HttpPost("{orderId}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Approve(string orderId)
        {
            if (this.User.IsInRole("Administrator"))
            {
                if (this._ordersService.Exists(orderId))
                {
                    await this._ordersService.ApproveOrderAsync(orderId);

                    return Ok(new
                    {
                        Message = "Order approved successfully."
                    });
                }

                return BadRequest(new BadRequestViewModel
                {
                    Message = "Order not found."
                });
            }

            return Unauthorized();
        }
    }
}
