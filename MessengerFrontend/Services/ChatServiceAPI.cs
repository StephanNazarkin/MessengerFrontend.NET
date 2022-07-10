using MessengerFrontend.Models.Chats;
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

            var chats = await JsonSerializer.DeserializeAsync<IEnumerable<ChatViewModel>>(contentStream);

            return chats;
        }

        public async Task<ChatViewModel> GetChatroom(int id)
        {
            var httpResponseMessage = await _httpClient.GetAsync("Chatroom/GetChatroom?chatId=" + id);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var chat = await JsonSerializer.DeserializeAsync<ChatViewModel>(contentStream);

            return chat;
        }
    }
}
