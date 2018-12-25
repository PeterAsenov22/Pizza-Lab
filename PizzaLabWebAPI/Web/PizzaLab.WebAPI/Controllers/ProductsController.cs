namespace PizzaLab.WebAPI.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Models.Products.ViewModels;
    using Services.DataServices.Contracts;
    using System.Collections.Generic;
    using System.Linq;  

    [Route("api/[controller]/[action]")]
    public class ProductsController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly IProductsService _productsService;

        public ProductsController(
            IMapper mapper,
            IProductsService productsService)
        {
            this._mapper = mapper;
            this._productsService = productsService;
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
    }
}
