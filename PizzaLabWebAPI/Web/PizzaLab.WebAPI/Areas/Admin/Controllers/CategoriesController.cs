namespace PizzaLab.WebAPI.Areas.Admin.Controllers
{   
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Models.Categories.InputModels;
    using Services.DataServices.Contracts;
    using System;
    using System.Threading.Tasks;
    using WebAPI.Controllers;
    using WebAPI.Models.Categories.ViewModels;
    using WebAPI.Models.Common;

    [Route("api/admin/[controller]")]
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

        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<SuccessViewModel<CategoryViewModel>>> Post([FromBody] CategoryInputModel model)
        {
            if (this.User.IsInRole("Administrator"))
            {
                try
                {
                    await this._categoriesService.CreateAsync(model.Name);

                    var createdCategoryDto = this._categoriesService.FindByName(model.Name);

                    return new SuccessViewModel<CategoryViewModel>
                    {
                        Data = this._mapper.Map<CategoryViewModel>(createdCategoryDto),
                        Message = "Category added successfully."
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
