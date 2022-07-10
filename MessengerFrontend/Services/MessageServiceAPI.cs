using MessengerFrontend.Models.Messages;
using MessengerFrontend.Services.Interfaces;
using System.Text.Json;

namespace MessengerFrontend.Services
{
    public class MessageServiceAPI : IMessageServiceAPI
    {
        private readonly HttpClient _httpClient;

        public MessageServiceAPI(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Messenger");
        }

        public async Task<IEnumerable<MessageViewModel>> GetMessagesFromChat(int chatId)
        {
            var httpResponseMessage = await _httpClient.GetAsync("Chatroom/GetMessagesFromChat?chatid=" + chatId);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var messages = await JsonSerializer.DeserializeAsync<IEnumerable<MessageViewModel>>(contentStream);

            return messages;
        }

        public async Task<MessageViewModel> SendMessage(MessageCreateModel model)
        {
            var httpResponseMessage = await _httpClient.PostAsJsonAsync("Message/SendMessage", model);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var message = await JsonSerializer.DeserializeAsync<MessageViewModel>(contentStream);

            return message;
        }
    }
}
