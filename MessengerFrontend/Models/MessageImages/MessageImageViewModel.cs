using System.Text.Json.Serialization;

namespace MessengerFrontend.Models.MessageImages
{
    public class MessageImageViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("path")]
        public string? Path { get; set; }
    }
}
