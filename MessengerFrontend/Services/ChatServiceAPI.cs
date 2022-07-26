using MessengerFrontend.Exceptions;
using MessengerFrontend.Models.Chats;
using MessengerFrontend.Models.UserAccounts;
using MessengerFrontend.Routes;
using MessengerFrontend.Services.Interfaces;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MessengerFrontend.Services
{
    public class ChatServiceAPI : BaseServiceAPI, IChatServiceAPI
    {
        #region Construcor
        
        public ChatServiceAPI(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, httpContextAccessor)
        { }
        
        #endregion

        #region Services

        public async Task<IEnumerable<ChatViewModel>> GetAllChatrooms()
        {
            var httpResponseMessage = await _httpClient.GetAsync(RoutesAPI.GetAllChatrooms);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new LoadChatsException("Sorry, we can't load your chats. It's most likely a server or connection issue.");
            }
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var chats = await JsonSerializer.DeserializeAsync<IEnumerable<ChatViewModel>>(contentStream);

            return chats;
        }

        public async Task<ChatViewModel> GetChatroom(int id)
        {
            var httpResponseMessage = await _httpClient.GetAsync(string.Format(RoutesAPI.GetChatroom, id));

            if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                throw new LoadChatsException("Sorry, we can't load this chat. It's most likely a server or connection issue.");
            }

            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            var chat = await JsonSerializer.DeserializeAsync<ChatViewModel>(contentStream);

            return chat;
        }

        public async Task<ChatViewModel> CreateChatroom(ChatCreateModel model)
        {
            if (string.IsNullOrEmpty(model.Topic))
            {
                throw new ChatroomException("Topic cannot be null");
            }

            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(model.Topic), "Topic");

            if (model.File is not null)
            {
                var file = model.File;
                var fileStream = new StreamContent(file.OpenReadStream());
                fileStream.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                content.Add(fileStream, "File", file.FileName);
            }
            
            var httpResponseMessage = await _httpClient.PostAsync(RoutesAPI.CreateChatroom, content);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new ChatroomException("Something went wrong, when you tried to create a new chat room.");
            }
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var response = await JsonSerializer.DeserializeAsync<ChatViewModel>(contentStream);

            return response;
        }

        public async Task<ChatViewModel> CreateAdminsChatroom(ChatCreateModel model)
        {
            if (string.IsNullOrEmpty(model.Topic))
            {
                throw new ChatroomException("Topic cannot be null");
            }

            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(model.Topic), "Topic");

            if (model.File is not null)
            {
                var file = model.File;
                var fileStream = new StreamContent(file.OpenReadStream());
                fileStream.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                content.Add(fileStream, "File", file.FileName);
            }

            var httpResponseMessage = await _httpClient.PostAsync(RoutesAPI.CreateAdminsChatroom, content);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new ChatroomException("Something went wrong, when you tried to create a new admin room.");
            }
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var response = await JsonSerializer.DeserializeAsync<ChatViewModel>(contentStream);

            return response;
        }

        public async Task<ChatViewModel> EditChatroom(ChatUpdateModel model)
        {
            if (string.IsNullOrEmpty(model.Topic))
            {
                throw new ChatroomException("Topic can't be null when you update your chat room.");
            }

            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(model.Topic), "Topic");

            content.Add(new StringContent(model.Id.ToString()), "Id");

            if (model.File is not null)
            {
                var file = model.File;
                var fileStream = new StreamContent(file.OpenReadStream());
                fileStream.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                content.Add(fileStream, "File", file.FileName);
            }
            
            var httpResponseMessage = await _httpClient.PutAsync(RoutesAPI.EditChatroom, content);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new ChatroomException("Something went wrong, when you tried to update your chat room.");
            }
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var response = await JsonSerializer.DeserializeAsync<ChatViewModel>(contentStream);

            return response;
        }

        public async Task<bool> DeleteChatroom(int chatId)
        {
            var httpResponseMessage = await _httpClient.PutAsJsonAsync(RoutesAPI.SoftDeleteChatroom, chatId);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new ChatroomException("Something went wrong, when you tried to delete your chat room.");
            }
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var response = await JsonSerializer.DeserializeAsync<bool>(contentStream);

            return response;
        }

        public async Task<IEnumerable<UserAccountViewModel>> GetAllMembers(int id)
        {
            var httpResponseMessage = await _httpClient.GetAsync(string.Format(RoutesAPI.GetAllUsersFromChat, id));
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new LoadUsersException("Sorry, we can't load members of this chat. It's most likely a server or connection issue.");
            }
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var response = await JsonSerializer.DeserializeAsync<IEnumerable<UserAccountViewModel>>(contentStream);

            return response;
        }

        public async Task<UserAccountViewModel> AddToChatroom(ChatInviteModel model)
        {
            var httpResponseMessage = await _httpClient.PostAsJsonAsync(RoutesAPI.AddToChatroom, model);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new MemberException("Something went wrong, when you tried to add a new member to your chat room.");
            }
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
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new MemberException("Something went wrong, when you tried to set a new admin in your chat room.");
            }
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var response = await JsonSerializer.DeserializeAsync<UserAccountViewModel>(contentStream);

            return response;
        }

        public async Task<UserAccountViewModel> UnsetAdmin(int userAccountId)
        {
            var httpResponseMessage = await _httpClient.PutAsJsonAsync(RoutesAPI.ChatUnsetAdmin, userAccountId);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new MemberException("Something went wrong, when you tried to unset this admin.");
            }
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var response = await JsonSerializer.DeserializeAsync<UserAccountViewModel>(contentStream);

            return response;
        }

        public async Task<UserAccountViewModel> MuteUser(int userAccountId)
        {
            var httpResponseMessage = await _httpClient.PutAsJsonAsync(RoutesAPI.ChatBanUser, userAccountId);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new MemberException("Something went wrong, when you tried to mute this user.");
            }
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var response = await JsonSerializer.DeserializeAsync<UserAccountViewModel>(contentStream);

            return response;
        }

        public async Task<UserAccountViewModel> UnmuteUser(int userAccountId)
        {
            var httpResponseMessage = await _httpClient.PutAsJsonAsync(RoutesAPI.ChatUnbanUser, userAccountId);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new MemberException("Something went wrong, when you tried to unmute this user.");
            }
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var response = await JsonSerializer.DeserializeAsync<UserAccountViewModel>(contentStream);

            return response;
        }

        public async Task<bool> KickUser(int userAccountId)
        {
            var httpResponseMessage = await _httpClient.PutAsJsonAsync(RoutesAPI.ChatKickUser, userAccountId);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new MemberException("Something went wrong, when you tried to kick this user.");
            }
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var response = await JsonSerializer.DeserializeAsync<bool>(contentStream);

            return response;
        }

        public async Task<bool> LeaveChat(int chatId)
        {
            var httpResponseMessage = await _httpClient.PutAsJsonAsync(RoutesAPI.LeaveFromChatroom, chatId);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new MemberException("Something went wrong, when you tried to leave from this chat.");
            }
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var response = await JsonSerializer.DeserializeAsync<bool>(contentStream);

            return response;
        }

        #endregion
    }
}
