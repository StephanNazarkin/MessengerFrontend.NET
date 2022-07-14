using MessengerFrontend.Models.Chats;
using MessengerFrontend.Models.UserAccounts;
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
            var httpResponseMessage = await _httpClient.GetAsync("Chatroom/GetAllChatrooms");
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var chats = await JsonSerializer.DeserializeAsync<IEnumerable<ChatViewModel>>(contentStream);

            return chats;
        }

        public async Task<ChatViewModel> GetChatroom(int id)
        {
            var httpResponseMessage = await _httpClient.GetAsync("Chatroom/GetChatroom?chatId=" + id);

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
            var httpResponseMessage = await _httpClient.PostAsJsonAsync("Chatroom/CreateChatroom", model);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var response = await JsonSerializer.DeserializeAsync<ChatViewModel>(contentStream);

            return response;
        }

        public async Task<ChatViewModel> EditChatroom(ChatUpdateModel model)
        {
            var httpResponseMessage = await _httpClient.PutAsJsonAsync("Chatroom/EditChatroom", model);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var response = await JsonSerializer.DeserializeAsync<ChatViewModel>(contentStream);

            return response;
        }

        public async Task<IEnumerable<UserAccountViewModel>> GetAllMembers(int id)
        {
            var httpResponseMessage = await _httpClient.GetAsync("Chatroom/GetAllUsers?chatId=" + id);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var response = await JsonSerializer.DeserializeAsync<IEnumerable<UserAccountViewModel>>(contentStream);

            return response;
        }

        public async Task<UserAccountViewModel> AddToChatroom(ChatInviteModel model)
        {
            var httpResponseMessage = await _httpClient.PostAsJsonAsync("Chatroom/AddToChatroom", model);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var response = await JsonSerializer.DeserializeAsync<UserAccountViewModel>(contentStream);

            return response;
        }

        public async Task<UserAccountViewModel> GetCurrentUserAccount(int chatId)
        {
            var httpResponseMessage = await _httpClient.GetAsync("Chatroom/GetCurrentUserAccount?chatId=" + chatId);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var response = await JsonSerializer.DeserializeAsync<UserAccountViewModel>(contentStream);

            return response;
        }

        public async Task<UserAccountViewModel> SetAdmin(int userAccountId)
        {
            var httpResponseMessage = await _httpClient.PutAsJsonAsync("Chatroom/SetAdmin", userAccountId);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var response = await JsonSerializer.DeserializeAsync<UserAccountViewModel>(contentStream);

            return response;
        }

        public async Task<UserAccountViewModel> UnsetAdmin(int userAccountId)
        {
            var httpResponseMessage = await _httpClient.PutAsJsonAsync("Chatroom/UnsetAdmin", userAccountId);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var response = await JsonSerializer.DeserializeAsync<UserAccountViewModel>(contentStream);

            return response;
        }

        public async Task<UserAccountViewModel> MuteUser(int userAccountId)
        {
            var httpResponseMessage = await _httpClient.PutAsJsonAsync("Chatroom/BanUser", userAccountId);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var response = await JsonSerializer.DeserializeAsync<UserAccountViewModel>(contentStream);

            return response;
        }

        public async Task<UserAccountViewModel> UnmuteUser(int userAccountId)
        {
            var httpResponseMessage = await _httpClient.PutAsJsonAsync("Chatroom/UnbanUser", userAccountId);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var response = await JsonSerializer.DeserializeAsync<UserAccountViewModel>(contentStream);

            return response;
        }

        public async Task<bool> KickUser(int userAccountId)
        {
            var httpResponseMessage = await _httpClient.PutAsJsonAsync("Chatroom/KickUser", userAccountId);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var response = await JsonSerializer.DeserializeAsync<bool>(contentStream);

            return response;
        }

        public async Task<bool> LeaveChat(int chatId)
        {
            var httpResponseMessage = await _httpClient.PutAsJsonAsync("Chatroom/LeaveFromChatroom", chatId);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var response = await JsonSerializer.DeserializeAsync<bool>(contentStream);

            return response;
        }
    }
}
