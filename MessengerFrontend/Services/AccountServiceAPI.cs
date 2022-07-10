using MessengerFrontend.Models.Users;
using MessengerFrontend.Services.Interfaces;
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

        public async Task<UserViewModel> GetCurrentUser()
        {
            var httpResponseMessage = await _httpClient.GetAsync("Account/GetCurrentUser");
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var user = await JsonSerializer.DeserializeAsync<UserViewModel>(contentStream);

            return user;
        }
    }
}
