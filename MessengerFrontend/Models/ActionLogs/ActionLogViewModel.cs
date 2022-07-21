using MessengerFrontend.Models.Users;
using System.Text.Json.Serialization;

namespace MessengerFrontend.Models.ActionLogs
{
    public class ActionLogViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("actionName")]
        public string ActionName { get; set; }

        [JsonPropertyName("user")]
        public UserViewModel User { get; set; }

        [JsonPropertyName("time")]
        public DateTime Time { get; set; }
    }
}
