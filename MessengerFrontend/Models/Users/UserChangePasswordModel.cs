using System.Text.Json.Serialization;

namespace MessengerFrontend.Models.Users
{
    public class UserChangePasswordModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("oldPassword")]
        public string OldPassword { get; set; }

        [JsonPropertyName("newPassword")]
        public string NewPassword { get; set; }
    }
}
