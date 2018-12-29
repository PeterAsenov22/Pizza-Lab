namespace PizzaLab.WebAPI.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;  
    using Models.Categories.ViewModels;
    using Services.DataServices.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class CategoriesController : ApiController
    {
        private readonly ICategoriesService _categoriesService;
        private readonly IMapper _mapper;

        public CategoriesController(
            ICategoriesService categoriesService,
            IMapper mapper)
        {
            this._categoriesService = categoriesService;
            this._mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<CategoryViewModel>> Get()
        {
            return this._categoriesService
                .All()
                .Select(c => this._mapper.Map<CategoryViewModel>(c))
                .ToList();
        }
    }
}
