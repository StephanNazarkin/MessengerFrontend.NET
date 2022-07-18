using MessengerFrontend.Models.ChatImages;
using MessengerFrontend.Models.Messages;
using MessengerFrontend.Models.UserAccounts;
using System.Text.Json.Serialization;

namespace MessengerFrontend.Models.Chats
{
    public class ChatViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("topic")]
        public string? Topic { get; set; }

        [JsonPropertyName("image")]
        public ChatImageViewModel? Image { get; set; }

        [JsonPropertyName("users")]
        public IEnumerable<UserAccountViewModel>? Users { get; set; }

        [JsonPropertyName("messages")]
        public IEnumerable<MessageViewModel>? Messages { get; set; }
    }
}
