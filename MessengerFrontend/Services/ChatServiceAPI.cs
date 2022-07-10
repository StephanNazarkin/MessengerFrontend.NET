using MessengerFrontend.Models.Chat;
using MessengerFrontend.Services.Interfaces;
using System.Text.Json;

namespace MessengerFrontend.Services
{
    public class ChatServiceAPI : IChatServiceAPI
    {
        private readonly HttpClient _httpClient;

        public ChatServiceAPI(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Messenger");
        }

        public async Task<IEnumerable<ChatViewModel>> GetAllChatrooms()
        {
            var httpResponseMessage = await _httpClient.GetAsync("Chatroom/GetAllChatrooms");
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var chats = await JsonSerializer.DeserializeAsync
                <IEnumerable<ChatViewModel>>(contentStream);

            return chats;
        }
    }
}
