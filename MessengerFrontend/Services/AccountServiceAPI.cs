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

        public async Task<UserViewModel> Login(UserViewModel model)
        {
            var httpResponseMessage = await _httpClient.PostAsJsonAsync("Account/Register", model);
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
    }
}
