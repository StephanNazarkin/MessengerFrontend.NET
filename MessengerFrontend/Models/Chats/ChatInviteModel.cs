using MessengerFrontend.Models.Messages;
using MessengerFrontend.Models.UserAccounts;
using System.Text.Json.Serialization;

namespace MessengerFrontend.Models.Chats
{
    public class ChatInviteModel
    {
        [JsonPropertyName("chatId")]
        public int ChatId { get; set; }
        [JsonPropertyName("userId")]
        public string? UserId { get; set; }
    }
}
