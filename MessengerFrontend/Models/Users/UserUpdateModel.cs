using System.Text.Json.Serialization;

namespace MessengerFrontend.Models.Users
{
    public class UserUpdateModel
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("userName")]
        public string? UserName { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }
    }
}
