using MessengerFrontend.Models.Messages;
using MessengerFrontend.Models.UserAccounts;
using System.Text.Json.Serialization;

namespace MessengerFrontend.Models.Chats
{
    public class ChatUpdateModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("topic")]
        public string? Topic { get; set; }
    }
}
