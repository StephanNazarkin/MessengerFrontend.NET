using MessengerFrontend.Models.Users;
using MessengerFrontend.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MessengerFrontend.Services
{
    public class AccountServiceAPI : IAccountServiceAPI
    {
        private readonly HttpClient _httpClient;

        public AccountServiceAPI(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Messenger");
        }

        public async Task<UserViewModel> Register(UserViewModel model) 
        {
            var httpResponseMessage = await _httpClient.PostAsJsonAsync("Account/Register", model);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var user = await JsonSerializer.DeserializeAsync<UserViewModel>(contentStream);


            return user;
        }

        public async Task<UserViewModel> Login(UserLoginModel model)
        {
            var httpResponseMessage = await _httpClient.PostAsJsonAsync("Account/Login", model);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            
            var user = await JsonSerializer.DeserializeAsync<UserViewModel>(contentStream);

            return user;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllFriends()
        {
            var httpResponseMessage = await _httpClient.GetAsync("Account/GetAllFriends");
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var friends = await JsonSerializer.DeserializeAsync
                <IEnumerable<UserViewModel>>(contentStream);

            return friends;

        }

        public async Task<IEnumerable<UserViewModel>> GetAllBlockedUsers()
        {
            var httpResponseMessage = await _httpClient.GetAsync("Account/GetAllBlockedUsers");
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var blockedUsers = await JsonSerializer.DeserializeAsync
                <IEnumerable<UserViewModel>>(contentStream);

            return blockedUsers;
        }
        
        public async Task<UserViewModel> GetCurrentUser()
        {
            var httpResponseMessage = await _httpClient.GetAsync("Account/GetCurrentUser");
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            var user = await JsonSerializer.DeserializeAsync<UserViewModel>(contentStream);

            return user;
        }

        public async Task<UserViewModel> GetUserByUserName(string userName)
        {
            var httpResponseMessage = await _httpClient.
                GetAsync(string.Format("/Account/GetUserByUserName?userName={0}", userName));
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            var user = await JsonSerializer.DeserializeAsync<UserViewModel>(contentStream);

            return user;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUsers()
        {
            var httpResponseMessage = await _httpClient.GetAsync("Account/GetAllUsers");
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var allUsers = await JsonSerializer.DeserializeAsync
                <IEnumerable<UserViewModel>>(contentStream);

            return allUsers;
        }

        public async Task<UserViewModel> AddFriend(string userId)
        {
            var httpResponseMessage = await _httpClient.PostAsJsonAsync("Account/AddFriend", userId);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var user = await JsonSerializer.DeserializeAsync<UserViewModel>(contentStream);

            return user;
        }

        public async Task<UserViewModel> DeleteFriend(string userId)
        {
            var httpResponseMessage = await _httpClient.DeleteAsync(string.Format("Account/DeleteFriend?friendId={0}", userId));
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var user = await JsonSerializer.DeserializeAsync<UserViewModel>(contentStream);

            return user;
        }

        public async Task<UserViewModel> BlockUser(string userId)
        {
            var httpResponseMessage = await _httpClient.PostAsJsonAsync("Account/BlockUser", userId);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var user = await JsonSerializer.DeserializeAsync<UserViewModel>(contentStream);

            return user;
        }

        public async Task<UserViewModel> UnblockUser(string userId)
        {
            var httpResponseMessage = await _httpClient.DeleteAsync(string.Format("Account/UnblockUser?blockedUserId={0}", userId));
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var user = await JsonSerializer.DeserializeAsync<UserViewModel>(contentStream);

            return user;
        }

        public async Task<UserViewModel> UpdateUser(UserUpdateModel userModel)
        {
            var httpResponseMessage = await _httpClient.PutAsJsonAsync("Account/UpdateUser", userModel);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var user = await JsonSerializer.DeserializeAsync<UserViewModel>(contentStream);

            return user;
        }
    }
}
