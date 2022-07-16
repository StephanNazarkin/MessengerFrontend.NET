using MessengerFrontend.Models.MessageImages;
using MessengerFrontend.Models.Users;
using System.Text.Json.Serialization;

namespace MessengerFrontend.Models.Messages
{
    public class MessageUpdateModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("text")]
        public string? Text { get; set; }
        [JsonPropertyName("chatId")]
        public int ChatId { get; set; }
    }
}
