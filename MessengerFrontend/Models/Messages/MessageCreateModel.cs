using MessengerFrontend.Models.Users;
using System.Text.Json.Serialization;

namespace MessengerFrontend.Models.Messages
{
    public class MessageCreateModel
    {
        [JsonPropertyName("chatId")]
        public int ChatId { get; set; }

        [JsonPropertyName("files")]
        public IFormFileCollection? Files { get; set; }

        [JsonPropertyName("text")]
        public string? Text { get; set; }
    }
}
