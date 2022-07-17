using System.Text.Json.Serialization;

namespace MessengerFrontend.Models.Chats
{
    public class ChatCreateModel
    {
        [JsonPropertyName("topic")]
        public string? Topic { get; set; }

        [JsonPropertyName("file")]
        public IFormFile? File { get; set; }
    }
}
