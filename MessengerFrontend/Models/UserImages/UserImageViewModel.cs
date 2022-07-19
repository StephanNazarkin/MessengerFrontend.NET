using System.Text.Json.Serialization;

namespace MessengerFrontend.Models.UserImages
{
    public class UserImageViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("path")]
        public string? Path { get; set; }
    }
}
