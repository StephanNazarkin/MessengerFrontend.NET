using System.Text.Json.Serialization;

namespace MessengerFrontend.Models.ChatImages
{
    public class ChatImageViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("path")]
        public string? Path { get; set; }
    }
}
