using MessengerFrontend.Models.Messages;
using MessengerFrontend.Routes;
using MessengerFrontend.Services.Interfaces;
using System.Net.Http.Headers;
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

        public async Task<MessageViewModel> GetMessage(int messageId)
        {
            var httpResponseMessage = await _httpClient.GetAsync(string.Format(RoutesAPI.GetMessage, messageId));
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var message = await JsonSerializer.DeserializeAsync<MessageViewModel>(contentStream);

            return message;
        }

        public async Task<IEnumerable<MessageViewModel>> GetMessagesFromChat(int chatId)
        {
            var httpResponseMessage = await _httpClient.GetAsync(string.Format(RoutesAPI.GetMessagesFromChat, chatId));
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var messages = await JsonSerializer.DeserializeAsync<IEnumerable<MessageViewModel>>(contentStream);

            return messages;
        }

        public async Task<bool> SendMessage(MessageCreateModel model)
        {
            if (model.Text is null && model.Files is null)
                return false;
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(model.ChatId.ToString()), "ChatId");

            if (model.Text is not null)
                content.Add(new StringContent(model.Text), "Text");

            if (model.Files is not null)
            {
                foreach (IFormFile file in model.Files)
                {
                    var fileStream = new StreamContent(file.OpenReadStream());
                    fileStream.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                    content.Add(fileStream, "files", file.FileName);
                }
            }
            
            var httpResponseMessage = await _httpClient.PostAsync(RoutesAPI.SendMessage, content);

            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var message = await JsonSerializer.DeserializeAsync<MessageViewModel>(contentStream);

            if (message is null)
                return false;

            return true;
        }

        public async Task<MessageViewModel> EditMessage(MessageUpdateModel model)
        {
            var httpResponseMessage = await _httpClient.PutAsJsonAsync(RoutesAPI.EditMessage, model);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var message = await JsonSerializer.DeserializeAsync<MessageViewModel>(contentStream);

            return message;
        }

        public async Task<bool> DeleteMessage(int id)
        {
            var httpResponseMessage = await _httpClient.PutAsJsonAsync(RoutesAPI.SoftDeleteMessage, id);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var result = await JsonSerializer.DeserializeAsync<bool>(contentStream);

            return result;
        }
    }
}
