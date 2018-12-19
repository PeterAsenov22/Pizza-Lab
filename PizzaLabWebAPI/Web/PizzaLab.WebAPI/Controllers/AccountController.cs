namespace PizzaLab.WebAPI.Controllers
{
    using AutoMapper;
    using Data.Models;
    using Helpers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using Models.Account.FacebookModels;
    using Models.Account.InputModels;
    using Models.Account.ViewModels;
    using Models.Common;
    using Newtonsoft.Json;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Net.Http;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    [AllowAnonymous]
    [Route("api/[controller]/[action]")]
    public class AccountController : ApiController
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly FacebookSettings _facebookSettings;
        private readonly JwtSettings _jwtSettings;
        private readonly IMapper _mapper;
        private readonly HttpClient _client;
 
        public AccountController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IOptions<FacebookSettings> facebookSettings,
            IOptions<JwtSettings> jwtSettings,
            IMapper mapper)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._facebookSettings = facebookSettings.Value;
            this._jwtSettings = jwtSettings.Value;
            this._mapper = mapper;
            this._client = new HttpClient();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<object> Login([FromBody] LoginInputModel model)
        {
            var user = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
            if (user is null)
            {
                return BadRequest(new BadRequestViewModel
                {
                    Message = "Incorrect e-mail or password."
                });
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, false);

            if (result.Succeeded)
            {
                return new AuthenticationViewModel
                {
                    Message = "You have successfully logged in.",
                    Token = GenerateJwtToken(user)
                };
            }

            return BadRequest(new BadRequestViewModel
            {
                Message = "Incorrect e-mail or password."
            });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<object> Register([FromBody] RegisterInputModel model)
        {
            var user = this._mapper.Map<ApplicationUser>(model);

            if (_userManager.Users.Any(u => u.Email == model.Email))
            {
                return BadRequest(new BadRequestViewModel
                {
                    Message = "This e-mail is already taken. Please try with another one."
                });
            }

            if (_userManager.Users.Any(u => u.UserName == model.Username))
            {
                return BadRequest(new BadRequestViewModel
                {
                    Message = "This username is already taken. Please try with another one."
                });
            }

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);

                return new AuthenticationViewModel
                {
                    Message = "You have successfully registered.",
                    Token = GenerateJwtToken(user)
                };
            }

            return BadRequest(new BadRequestViewModel
            {
                Message = "Something went wrong. Check your form and try again"
            });
        }

        [Route("/api/account/login/external/facebook")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<object> FacebookLogin([FromBody] FacebookLoginInputModel model)
        {
            var appAccessTokenResponse = await _client.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={_facebookSettings.AppId}&client_secret={_facebookSettings.AppSecret}&grant_type=client_credentials");

            var appAccessToken = JsonConvert.DeserializeObject<FacebookAppAccessToken>(appAccessTokenResponse);

            var userAccessTokenValidationResponse = await _client.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={model.AccessToken}&access_token={appAccessToken.AccessToken}");
            var userAccessTokenValidation = JsonConvert.DeserializeObject<FacebookUserAccessTokenValidation>(userAccessTokenValidationResponse);

            if (!userAccessTokenValidation.Data.IsValid)
            {
                throw new Exception("Invalid Facebook Token.");
            }

            var userInfoResponse = await _client.GetStringAsync($"https://graph.facebook.com/v2.8/me?fields=id,email,first_name,last_name,name,gender,locale,birthday,picture&access_token={model.AccessToken}");
            var userInfo = JsonConvert.DeserializeObject<FacebookUserData>(userInfoResponse);

            if (userInfo.Email is null)
            {
                var userFullName = $"{userInfo.FirstName.ToLower()}{userInfo.LastName.ToLower()}";
                userInfo.Email = $"{userFullName}@facebook.com";
                if (!_userManager.Users.Any(u => u.UserName == userFullName))
                {
                    userInfo.Username = userFullName;
                }
                else
                {
                    userInfo.Username = userInfo.Email;
                }
            }

            var user = await this._userManager.FindByEmailAsync(userInfo.Email);
            if (user is null)
            {
                user = this._mapper.Map<ApplicationUser>(userInfo);
                if (_userManager.Users.Any(u => u.Email == user.Email))
                {
                    return BadRequest(new BadRequestViewModel
                    {
                        Message = "This e-mail is already taken."
                    });
                }

                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    return BadRequest(new BadRequestViewModel
                    {
                        Message = "Something went wrong."
                    });
                }
            }

            return new AuthenticationViewModel
            {
                Message = "You have successfully logged in.",
                Token = GenerateJwtToken(user)
            };
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
           
            var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Role, 
                            this._userManager.IsInRoleAsync(user, "Administrator")
                                .GetAwaiter()
                                .GetResult() ? "Administrator" : "User")
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
