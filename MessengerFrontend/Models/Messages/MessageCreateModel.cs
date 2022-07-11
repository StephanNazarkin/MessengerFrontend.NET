using MessengerFrontend.Models.Users;
using System.Text.Json.Serialization;

namespace MessengerFrontend.Models.Messages
{
    public class MessageCreateModel
    {
        [JsonPropertyName("chatId")]
        public int? ChatId { get; set; }
        [JsonPropertyName("userId")]
        public string? UserId { get; set; }
        //public ICollection<MessageImageViewModel> images { get; set; }
        [JsonPropertyName("text")]
        public string? Text { get; set; }
    }
}
