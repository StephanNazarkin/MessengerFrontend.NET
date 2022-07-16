using MessengerFrontend.Models.Chats;
using MessengerFrontend.Models.UserAccounts;
using MessengerFrontend.Routes;
using MessengerFrontend.Services.Interfaces;
using System.Net;
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
            var httpResponseMessage = await _httpClient.GetAsync(RoutesAPI.GetAllChatrooms);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var chats = await JsonSerializer.DeserializeAsync<IEnumerable<ChatViewModel>>(contentStream);

            return chats;
        }

        public async Task<ChatViewModel> GetChatroom(int id)
        {
            var httpResponseMessage = await _httpClient.GetAsync(string.Format(RoutesAPI.GetChatroom, id));

            if (httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                throw new KeyNotFoundException();
            }

            if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Something went wrong");
            }

            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            var chat = await JsonSerializer.DeserializeAsync<ChatViewModel>(contentStream);

            if (chat is null)
                throw new Exception("Null exception");

            return chat;
        }

        public async Task<ChatViewModel> CreateChatroom(ChatCreateModel model)
        {
            var httpResponseMessage = await _httpClient.PostAsJsonAsync(RoutesAPI.CreateChatroom, model);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var response = await JsonSerializer.DeserializeAsync<ChatViewModel>(contentStream);

            return response;
        }

        public async Task<ChatViewModel> EditChatroom(ChatUpdateModel model)
        {
            var httpResponseMessage = await _httpClient.PutAsJsonAsync(RoutesAPI.EditChatroom, model);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var response = await JsonSerializer.DeserializeAsync<ChatViewModel>(contentStream);

            return response;
        }

        public async Task<bool> DeleteChatroom(int chatId)
        {
            var httpResponseMessage = await _httpClient.PutAsJsonAsync(RoutesAPI.SoftDeleteChatroom, chatId);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var response = await JsonSerializer.DeserializeAsync<bool>(contentStream);

            return response;
        }

        public async Task<IEnumerable<UserAccountViewModel>> GetAllMembers(int id)
        {
            var httpResponseMessage = await _httpClient.GetAsync(string.Format(RoutesAPI.GetAllUsersFromChat, id));
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var response = await JsonSerializer.DeserializeAsync<IEnumerable<UserAccountViewModel>>(contentStream);

            return response;
        }

        public async Task<UserAccountViewModel> AddToChatroom(ChatInviteModel model)
        {
            var httpResponseMessage = await _httpClient.PostAsJsonAsync(RoutesAPI.AddToChatroom, model);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var response = await JsonSerializer.DeserializeAsync<UserAccountViewModel>(contentStream);

            return response;
        }

        public async Task<UserAccountViewModel> GetCurrentUserAccount(int chatId)
        {
            var httpResponseMessage = await _httpClient.GetAsync(string.Format(RoutesAPI.GetCurrentUserAccount, chatId));
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var response = await JsonSerializer.DeserializeAsync<UserAccountViewModel>(contentStream);

            return response;
        }

        public async Task<UserAccountViewModel> SetAdmin(int userAccountId)
        {
            var httpResponseMessage = await _httpClient.PutAsJsonAsync(RoutesAPI.ChatSetAdmin, userAccountId);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var response = await JsonSerializer.DeserializeAsync<UserAccountViewModel>(contentStream);

            return response;
        }

        public async Task<UserAccountViewModel> UnsetAdmin(int userAccountId)
        {
            var httpResponseMessage = await _httpClient.PutAsJsonAsync(RoutesAPI.ChatUnsetAdmin, userAccountId);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var response = await JsonSerializer.DeserializeAsync<UserAccountViewModel>(contentStream);

            return response;
        }

        public async Task<UserAccountViewModel> MuteUser(int userAccountId)
        {
            var httpResponseMessage = await _httpClient.PutAsJsonAsync(RoutesAPI.ChatBanUser, userAccountId);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var response = await JsonSerializer.DeserializeAsync<UserAccountViewModel>(contentStream);

            return response;
        }

        public async Task<UserAccountViewModel> UnmuteUser(int userAccountId)
        {
            var httpResponseMessage = await _httpClient.PutAsJsonAsync(RoutesAPI.ChatUnbanUser, userAccountId);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var response = await JsonSerializer.DeserializeAsync<UserAccountViewModel>(contentStream);

            return response;
        }

        public async Task<bool> KickUser(int userAccountId)
        {
            var httpResponseMessage = await _httpClient.PutAsJsonAsync(RoutesAPI.ChatKickUser, userAccountId);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var response = await JsonSerializer.DeserializeAsync<bool>(contentStream);

            return response;
        }

        public async Task<bool> LeaveChat(int chatId)
        {
            var httpResponseMessage = await _httpClient.PutAsJsonAsync(RoutesAPI.LeaveFromChatroom, chatId);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var response = await JsonSerializer.DeserializeAsync<bool>(contentStream);

            return response;
        }
    }
}
