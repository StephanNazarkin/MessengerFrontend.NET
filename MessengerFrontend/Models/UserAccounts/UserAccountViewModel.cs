using MessengerFrontend.Models.Chats;
using MessengerFrontend.Models.Users;
using System.Text.Json.Serialization;

namespace MessengerFrontend.Models.UserAccounts
{
    public class UserAccountViewModel
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }
        [JsonPropertyName("user")]
        public UserViewModel? User { get; set; }
        [JsonPropertyName("chat")]
        public ChatViewModel? Password { get; set; }
        [JsonPropertyName("isBanned")]
        public bool IsBanned { get; set; }
        [JsonPropertyName("isAdmin")]
        public bool IsAdmin { get; set; }
        [JsonPropertyName("isOwner")]
        public bool IsOwner { get; set; }
    }
}
