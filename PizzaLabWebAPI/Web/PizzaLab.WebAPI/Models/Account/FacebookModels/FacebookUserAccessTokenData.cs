namespace PizzaLab.WebAPI.Models.Account.FacebookModels
{
    using Newtonsoft.Json;

    public class FacebookUserAccessTokenData
    {
        [JsonProperty("is_valid")]
        public bool IsValid { get; set; }
    }
}
