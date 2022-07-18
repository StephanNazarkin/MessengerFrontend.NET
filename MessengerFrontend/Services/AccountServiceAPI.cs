using MessengerFrontend.Models.Users;
using MessengerFrontend.Routes;
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
            var httpResponseMessage = await _httpClient.PostAsJsonAsync(RoutesAPI.Register, model);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var user = await JsonSerializer.DeserializeAsync<UserViewModel>(contentStream);


            return user;
        }

        public async Task<UserViewModel> Login(UserLoginModel model)
        {
            var httpResponseMessage = await _httpClient.PostAsJsonAsync(RoutesAPI.Login, model);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            
            var user = await JsonSerializer.DeserializeAsync<UserViewModel>(contentStream);

            return user;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllFriends(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpResponseMessage = await _httpClient.GetAsync(RoutesAPI.GetAllFriends);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var friends = await JsonSerializer.DeserializeAsync
                <IEnumerable<UserViewModel>>(contentStream);

            return friends;

        }

        public async Task<IEnumerable<UserViewModel>> GetAllBlockedUsers(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpResponseMessage = await _httpClient.GetAsync(RoutesAPI.GetAllBlockedUsers);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var blockedUsers = await JsonSerializer.DeserializeAsync
                <IEnumerable<UserViewModel>>(contentStream);

            return blockedUsers;
        }
        
        public async Task<UserViewModel> GetCurrentUser(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpResponseMessage = await _httpClient.GetAsync(RoutesAPI.GetCurrentUser);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            var user = await JsonSerializer.DeserializeAsync<UserViewModel>(contentStream);

            return user;
        }

        public async Task<UserViewModel> GetUserByUserName(string userName, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpResponseMessage = await _httpClient.
                GetAsync(string.Format(RoutesAPI.GetUserByUserName, userName));
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            var user = await JsonSerializer.DeserializeAsync<UserViewModel>(contentStream);

            return user;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUsers(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpResponseMessage = await _httpClient.GetAsync(RoutesAPI.GetAllUsers);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var allUsers = await JsonSerializer.DeserializeAsync
                <IEnumerable<UserViewModel>>(contentStream);

            return allUsers;
        }

        public async Task<UserViewModel> AddFriend(string userId, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpResponseMessage = await _httpClient.PostAsJsonAsync(RoutesAPI.AddFriend, userId);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var user = await JsonSerializer.DeserializeAsync<UserViewModel>(contentStream);

            return user;
        }

        public async Task<UserViewModel> DeleteFriend(string userId, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpResponseMessage = await _httpClient.DeleteAsync(string.Format(RoutesAPI.DeleteFriend, userId));
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var user = await JsonSerializer.DeserializeAsync<UserViewModel>(contentStream);

            return user;
        }

        public async Task<UserViewModel> BlockUser(string userId, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpResponseMessage = await _httpClient.PostAsJsonAsync(RoutesAPI.BlockUser, userId);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var user = await JsonSerializer.DeserializeAsync<UserViewModel>(contentStream);

            return user;
        }

        public async Task<UserViewModel> UnblockUser(string userId, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpResponseMessage = await _httpClient.DeleteAsync(string.Format(RoutesAPI.UnblockUser, userId));
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var user = await JsonSerializer.DeserializeAsync<UserViewModel>(contentStream);

            return user;
        }

        public async Task<UserViewModel> UpdateUser(UserUpdateModel userModel, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpResponseMessage = await _httpClient.PutAsJsonAsync(RoutesAPI.UpdateUser, userModel);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var user = await JsonSerializer.DeserializeAsync<UserViewModel>(contentStream);

            return user;
        }
    }
}
