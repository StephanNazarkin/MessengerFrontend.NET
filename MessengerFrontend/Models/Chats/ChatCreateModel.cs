using MessengerFrontend.Models.Messages;
using MessengerFrontend.Models.UserAccounts;
using System.Text.Json.Serialization;

namespace MessengerFrontend.Models.Chats
{
    public class ChatCreateModel
    {
        [JsonPropertyName("topic")]
        public string? Topic { get; set; }
    }
}
