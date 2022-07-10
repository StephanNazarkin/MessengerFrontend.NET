using System.Text.Json.Serialization;

namespace MessengerFrontend.Models.Users
{
    public class UserViewModel
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("userName")]
        public string? UserName { get; set; }
        [JsonPropertyName("password")]
        public string? Password { get; set; }
        [JsonPropertyName("email")]
        public string? Email { get; set; }
        [JsonPropertyName("token")]
        public string? Token { get; set; }
    }
}
