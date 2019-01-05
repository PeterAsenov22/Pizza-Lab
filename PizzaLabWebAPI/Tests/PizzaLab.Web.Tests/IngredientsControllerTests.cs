namespace PizzaLab.Web.Tests
{
    using Helpers;
    using Newtonsoft.Json; 
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;    
    using WebAPI.Areas.Admin.Models.Ingredients.InputModels;
    using WebAPI.Models.Account.InputModels;
    using WebAPI.Models.Account.ViewModels;
    using WebAPI.Models.Common;
    using WebAPI.Models.Ingredients.ViewModels;
    using Xunit;

    public class IngredientsControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;

        public IngredientsControllerTests(CustomWebApplicationFactory factory)
        {
            this._factory = factory;
        }

        [Fact]
        public async Task GetMethodShouldReturnAllIngredients()
        {
            var client = this._factory.CreateClient();
            var response = await client.GetAsync("/api/ingredients");
            var content = await response.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<IEnumerable<IngredientViewModel>>(content).ToArray();

            response.EnsureSuccessStatusCode();
            Assert.True(categories.Count() >= 31); 
        }

        [Fact]
        public async Task PostMethodShouldCreateIngredientSuccessfully()
        {
            var client = this._factory.CreateClient();
            var loginInputModel = new LoginInputModel
            {
                Email = "admin@admin.com",
                Password = "12345678"
            };

            var loginRequestContent = new StringContent(
                JsonConvert.SerializeObject(loginInputModel),
                Encoding.UTF8,
                "application/json");

            var loginResponse = await client.PostAsync("/api/account/login", loginRequestContent);

            var loginContent = await loginResponse.Content.ReadAsStringAsync();
            var authObj = JsonConvert.DeserializeObject<AuthenticationViewModel>(loginContent);

            var ingredientInputModel = new IngredientInputModel
            {
                Name = "ham"
            };

            var createIngredientRequestContent = new StringContent(
                JsonConvert.SerializeObject(ingredientInputModel),
                Encoding.UTF8,
                "application/json");

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {authObj.Token}");

            var createIngredientResponse = await client.PostAsync("/api/admin/ingredients", createIngredientRequestContent);
            createIngredientResponse.EnsureSuccessStatusCode();

            var createIngredientContent = await createIngredientResponse.Content.ReadAsStringAsync();
            var ingredientViewModel = JsonConvert.DeserializeObject<SuccessViewModel<IngredientViewModel>>(createIngredientContent);

            Assert.Equal("Ingredient added successfully.", ingredientViewModel.Message);
            Assert.Equal("ham", ingredientViewModel.Data.Name);
        }

        [Fact]
        public async Task PostMethodShouldReturnUnauthorized()
        {
            var client = this._factory.CreateClient();
            var ingredientInputModel = new IngredientInputModel
            {
                Name = "ham"
            };

            var createIngredientRequestContent = new StringContent(
                JsonConvert.SerializeObject(ingredientInputModel),
                Encoding.UTF8,
                "application/json");

            var createIngredientResponse = await client.PostAsync("/api/admin/ingredients", createIngredientRequestContent);
            Assert.Equal("Unauthorized", createIngredientResponse.StatusCode.ToString());
        }

        [Fact]
        public async Task PostMethodShouldReturnBadRequest()
        {
            var client = this._factory.CreateClient();
            var loginInputModel = new LoginInputModel
            {
                Email = "admin@admin.com",
                Password = "12345678"
            };

            var loginRequestContent = new StringContent(
                JsonConvert.SerializeObject(loginInputModel),
                Encoding.UTF8,
                "application/json");

            var loginResponse = await client.PostAsync("/api/account/login", loginRequestContent);

            var loginContent = await loginResponse.Content.ReadAsStringAsync();
            var authObj = JsonConvert.DeserializeObject<AuthenticationViewModel>(loginContent);

            var ingredientInputModel = new IngredientInputModel
            {
                Name = "ha"
            };

            var createIngredientRequestContent = new StringContent(
                JsonConvert.SerializeObject(ingredientInputModel),
                Encoding.UTF8,
                "application/json");

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {authObj.Token}");

            var createIngredientResponse = await client.PostAsync("/api/admin/categories", createIngredientRequestContent);
            Assert.Equal("BadRequest", createIngredientResponse.StatusCode.ToString());
        }
    }
}
