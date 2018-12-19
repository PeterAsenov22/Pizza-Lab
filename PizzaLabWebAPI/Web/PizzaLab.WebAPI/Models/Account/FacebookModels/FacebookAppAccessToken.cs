namespace PizzaLab.WebAPI.Models.Account.FacebookModels
{
    using Newtonsoft.Json;

    public class FacebookAppAccessToken
    {
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}
