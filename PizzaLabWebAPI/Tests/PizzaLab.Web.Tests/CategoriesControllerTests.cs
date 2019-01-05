namespace PizzaLab.Web.Tests
{
    using Helpers;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using WebAPI.Areas.Admin.Models.Categories.InputModels;
    using WebAPI.Models.Account.InputModels;
    using WebAPI.Models.Account.ViewModels;
    using WebAPI.Models.Categories.ViewModels;
    using WebAPI.Models.Common;
    using Xunit;

    public class CategoriesControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;

        public CategoriesControllerTests(CustomWebApplicationFactory factory)
        {
            this._factory = factory;
        }

        [Fact]
        public async Task GetMethodShouldReturnAllCategories()
        {
            var client = this._factory.CreateClient();
            var response = await client.GetAsync("/api/categories");
            var content = await response.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<IEnumerable<CategoryViewModel>>(content).ToArray();

            response.EnsureSuccessStatusCode();
            Assert.Equal(5, categories.Count());
            Assert.Contains(categories, c => c.Name == "Traditional");
            Assert.Contains(categories, c => c.Name == "Vegetarian");
            Assert.Contains(categories, c => c.Name == "Italian");
            Assert.Contains(categories, c => c.Name == "American");
            Assert.Contains(categories, c => c.Name == "Premium");
        }

        [Fact]
        public async Task PostMethodShouldCreateCategorySuccessfully()
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

            var categoryInputModel = new CategoryInputModel
            {
                Name = "Seafood"
            };

            var createCategoryRequestContent = new StringContent(
                JsonConvert.SerializeObject(categoryInputModel),
                Encoding.UTF8,
                "application/json");

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {authObj.Token}");

            var createCategoryResponse = await client.PostAsync("/api/admin/categories", createCategoryRequestContent);
            createCategoryResponse.EnsureSuccessStatusCode();

            var createCategoryContent = await createCategoryResponse.Content.ReadAsStringAsync();
            var categoryViewModel = JsonConvert.DeserializeObject<SuccessViewModel<CategoryViewModel>>(createCategoryContent);

            Assert.Equal("Category added successfully.", categoryViewModel.Message);
            Assert.Equal("Seafood", categoryViewModel.Data.Name);
        }

        [Fact]
        public async Task PostMethodShouldReturnUnauthorized()
        {
            var client = this._factory.CreateClient();
            var categoryInputModel = new CategoryInputModel
            {
                Name = "Seafood"
            };

            var createCategoryRequestContent = new StringContent(
                JsonConvert.SerializeObject(categoryInputModel),
                Encoding.UTF8,
                "application/json");

            var createCategoryResponse = await client.PostAsync("/api/admin/categories", createCategoryRequestContent);
            Assert.Equal("Unauthorized", createCategoryResponse.StatusCode.ToString());
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

            var categoryInputModel = new CategoryInputModel
            {
                Name = "Se"
            };

            var createCategoryRequestContent = new StringContent(
                JsonConvert.SerializeObject(categoryInputModel),
                Encoding.UTF8,
                "application/json");

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {authObj.Token}");

            var createCategoryResponse = await client.PostAsync("/api/admin/categories", createCategoryRequestContent);
            Assert.Equal("BadRequest", createCategoryResponse.StatusCode.ToString());
        }
    }
}
