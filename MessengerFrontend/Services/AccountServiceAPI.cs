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
        public async Task<IEnumerable<UserViewModel>> GetAllFriends()
        {
            var httpResponseMessage = await _httpClient.GetAsync("Account/GetAllFriends");
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var friends = await JsonSerializer.DeserializeAsync
                <IEnumerable<UserViewModel>>(contentStream);
            //if(friends == null)
                //return null;

            return friends;

        }
    }
}
