using MessengerFrontend.Models.MessageImages;
using MessengerFrontend.Models.Users;
using System.Text.Json.Serialization;

namespace MessengerFrontend.Models.Messages
{
    public class MessageViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("user")]
        public UserViewModel? User { get; set; }

        [JsonPropertyName("images")]
        public ICollection<MessageImageViewModel>? Images { get; set; }

        [JsonPropertyName("text")]
        public string? Text { get; set; }

        [JsonPropertyName("createdTime")]
        public DateTime CreatedTime { get; set; }
    }
}
