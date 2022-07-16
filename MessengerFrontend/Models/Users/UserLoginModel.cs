using System.Text.Json.Serialization;

namespace MessengerFrontend.Models.Users
{
    public class UserLoginModel
    {
        [JsonPropertyName("userName")]
        public string? UserName { get; set; }
        [JsonPropertyName("password")]
        public string? Password { get; set; }
    }
}
