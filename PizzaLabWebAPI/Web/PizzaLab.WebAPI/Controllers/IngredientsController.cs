namespace PizzaLab.WebAPI.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc; 
    using Models.Ingredients.ViewModels;
    using Services.DataServices.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class IngredientsController : ApiController
    {
        private readonly IIngredientsService _ingredientsService;
        private readonly IMapper _mapper;

        public IngredientsController(
            IIngredientsService ingredientsService,
            IMapper mapper)
        {
            this._ingredientsService = ingredientsService;
            this._mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<IngredientViewModel>> Get()
        {
            return this._ingredientsService
                .All()
                .Select(i => this._mapper.Map<IngredientViewModel>(i))
                .ToList();
        }
    }
}
