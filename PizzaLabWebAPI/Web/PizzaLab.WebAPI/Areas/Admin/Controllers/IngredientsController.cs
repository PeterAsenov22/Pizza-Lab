namespace PizzaLab.WebAPI.Areas.Admin.Controllers
{ 
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Models.Ingredients.InputModels;
    using Services.DataServices.Contracts;
    using System;
    using System.Threading.Tasks;
    using WebAPI.Controllers;
    using WebAPI.Models.Common;
    using WebAPI.Models.Ingredients.ViewModels;

    [Route("api/admin/[controller]")]
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

        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<SuccessViewModel<IngredientViewModel>>> Post([FromBody] IngredientInputModel model)
        {
            if (this.User.IsInRole("Administrator"))
            {
                try
                {
                    await this._ingredientsService.CreateAsync(model.Name);

                    var createdIngredientDto = this._ingredientsService.FindByName(model.Name);

                    return new SuccessViewModel<IngredientViewModel>
                    {
                        Data = this._mapper.Map<IngredientViewModel>(createdIngredientDto),
                        Message = "Ingredient added successfully."
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

            return Unauthorized();
        }
    }
}
