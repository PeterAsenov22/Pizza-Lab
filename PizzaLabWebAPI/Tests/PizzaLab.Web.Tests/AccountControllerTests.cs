namespace PizzaLab.Web.Tests
{
    using Helpers;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json; 
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;  
    using WebAPI.Models.Account.InputModels;
    using WebAPI.Models.Account.ViewModels;
    using WebAPI.Models.Common;
    using Xunit;

    public class AccountControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory factory;

        public AccountControllerTests(CustomWebApplicationFactory factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task RegisterMethodShouldRegisterUserSuccessfully()
        {
            var client = this.factory.CreateClient();
            var registerInputModel = new RegisterInputModel
            {
                Email = "vankata@abv.bg",
                FirstName = "Ivancho",
                LastName = "Nenov",
                Username = "vankata",
                Password = "12345678"
            };

            var stringContent = new StringContent(
                JsonConvert.SerializeObject(registerInputModel),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync("/api/account/register", stringContent);

            var content = await response.Content.ReadAsStringAsync();
            var authObj = JsonConvert.DeserializeObject<AuthenticationViewModel>(content);

            response.EnsureSuccessStatusCode();
            Assert.Equal("You have successfully registered.", authObj.Message);
            Assert.NotNull(authObj.Token);
            Assert.Equal(332, authObj.Token.Length);
        }

        [Fact]
        public async Task RegisterMethodShouldFailWhenTryingToRegisterWithAlreadyExistingEmail()
        {
            var client = this.factory.CreateClient();
            var registerInputModel = new RegisterInputModel
            {
                Email = "vankata@abv.bg",
                FirstName = "Ivan",
                LastName = "Nenovv",
                Username = "vankata2",
                Password = "12341234"
            };

            var stringContent = new StringContent(
                JsonConvert.SerializeObject(registerInputModel),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync("/api/account/register", stringContent);

            var content = await response.Content.ReadAsStringAsync();
            var badRequestObj = JsonConvert.DeserializeObject<BadRequestViewModel>(content);

            Assert.Equal("This e-mail is already taken. Please try with another one.", badRequestObj.Message);
            Assert.Equal(400, badRequestObj.Status);
        }

        [Fact]
        public async Task RegisterMethodShouldFailWhenTryingToRegisterWithAlreadyExistingUsername()
        {
            var client = this.factory.CreateClient();
            var registerInputModel = new RegisterInputModel
            {
                Email = "vankata1@abv.bg",
                FirstName = "Ivan",
                LastName = "Nenov",
                Username = "vankata",
                Password = "12341234"
            };

            var stringContent = new StringContent(
                JsonConvert.SerializeObject(registerInputModel),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync("/api/account/register", stringContent);

            var content = await response.Content.ReadAsStringAsync();
            var badRequestObj = JsonConvert.DeserializeObject<BadRequestViewModel>(content);

            Assert.Equal("This username is already taken. Please try with another one.", badRequestObj.Message);
            Assert.Equal(400, badRequestObj.Status);
        }

        [Theory]
        [InlineData("vankata", "vankata", "Ivan", "Nenov", "12345678")]
        [InlineData(null, "vankata", "Ivan", "Nenov", "12345678")]
        [InlineData("vankata@abv.bg", "va", "Ivan", "Nenov", "12345678")]
        [InlineData("vankata@abv.bg", "vankata", "I", "Nenov", "12345678")]
        [InlineData("vankata@abv.bg", "vankata", "Ivan", "N", "12345678")]
        [InlineData("vankata@abv.bg", "vankata", "Ivan", "Nenov", "1234567")]
        public async Task RegisterMethodShouldFailWithInvalidParameter(
            string email,
            string username,
            string firstName,
            string lastName,
            string password
            )
        {
            var client = this.factory.CreateClient();
            var registerInputModel = new RegisterInputModel
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Username = username,
                Password = password
            };

            var stringContent = new StringContent(
                JsonConvert.SerializeObject(registerInputModel),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync("/api/account/register", stringContent);

            var content = await response.Content.ReadAsStringAsync();
            var problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(content);

            Assert.Equal("One or more validation errors occurred.", problemDetails.Title);
            Assert.Equal(400, problemDetails.Status);
        }

        [Fact]
        public async Task LoginMethodShouldLoginUserSuccessfully()
        {
            var client = this.factory.CreateClient();
            var loginInputModel = new LoginInputModel
            {
                Email = "vankata@abv.bg",
                Password = "12345678"
            };

            var stringContent = new StringContent(
                JsonConvert.SerializeObject(loginInputModel),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync("/api/account/login", stringContent);

            var content = await response.Content.ReadAsStringAsync();
            var authObj = JsonConvert.DeserializeObject<AuthenticationViewModel>(content);

            response.EnsureSuccessStatusCode();
            Assert.Equal("You have successfully logged in.", authObj.Message);
            Assert.NotNull(authObj.Token);
            Assert.Equal(332, authObj.Token.Length);
        }

        [Theory]
        [InlineData("email", "vankataa@abv.bg")]
        [InlineData("password", "123456788")]
        public async Task LoginMethodShouldFailWithInvalidParameter(string invalidParamType, string invalidParamData)
        {
            var client = this.factory.CreateClient();
            LoginInputModel loginInputModel = new LoginInputModel();

            if (invalidParamType.Equals("email"))
            {
                loginInputModel.Email = invalidParamData;
                loginInputModel.Password = "12345678";
            } else if (invalidParamType.Equals("password"))
            {
                loginInputModel.Email = "vankata@abv.bg";
                loginInputModel.Password = invalidParamData;
            }           

            var stringContent = new StringContent(
                JsonConvert.SerializeObject(loginInputModel),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync("/api/account/login", stringContent);

            var content = await response.Content.ReadAsStringAsync();
            var badRequestObj = JsonConvert.DeserializeObject<BadRequestViewModel>(content);

            Assert.Equal("Incorrect e-mail or password.", badRequestObj.Message);
            Assert.Equal(400, badRequestObj.Status);
        }
    }
}
