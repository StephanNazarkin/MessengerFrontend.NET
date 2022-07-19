using MessengerFrontend.Exceptions;
using MessengerFrontend.Filters;
using MessengerFrontend.Models.Users;
using MessengerFrontend.Routes;
using MessengerFrontend.Services.Interfaces;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MessengerFrontend.Services
{
    public class AccountServiceAPI : BaseServiceAPI, IAccountServiceAPI
    {
        public AccountServiceAPI(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, httpContextAccessor)
        { }

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
            if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                throw new LoginException("You caused a login error!!!");
            }
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            var user = await JsonSerializer.DeserializeAsync<UserViewModel>(contentStream);

            return user;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllFriends()
        {
            var httpResponseMessage = await _httpClient.GetAsync(RoutesAPI.GetAllFriends);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            var friends = await JsonSerializer.DeserializeAsync
                <IEnumerable<UserViewModel>>(contentStream);

            return friends;

        }

        public async Task<IEnumerable<UserViewModel>> GetAllBlockedUsers()
        {
            var httpResponseMessage = await _httpClient.GetAsync(RoutesAPI.GetAllBlockedUsers);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var blockedUsers = await JsonSerializer.DeserializeAsync
                <IEnumerable<UserViewModel>>(contentStream);

            return blockedUsers;
        }
        
        public async Task<UserViewModel> GetCurrentUser()
        {
            var httpResponseMessage = await _httpClient.GetAsync(RoutesAPI.GetCurrentUser);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            var user = await JsonSerializer.DeserializeAsync<UserViewModel>(contentStream);

            return user;
        }

        public async Task<UserViewModel> GetUserByUserName(string userName)
        {
            var httpResponseMessage = await _httpClient.
                GetAsync(string.Format(RoutesAPI.GetUserByUserName, userName));
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            var user = await JsonSerializer.DeserializeAsync<UserViewModel>(contentStream);

            return user;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUsers()
        {
            var httpResponseMessage = await _httpClient.GetAsync(RoutesAPI.GetAllUsers);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            var allUsers = await JsonSerializer.DeserializeAsync
                <IEnumerable<UserViewModel>>(contentStream);

            return allUsers;
        }

        public async Task<UserViewModel> AddFriend(string userId)
        {
            var httpResponseMessage = await _httpClient.PostAsJsonAsync(RoutesAPI.AddFriend, userId);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            var user = await JsonSerializer.DeserializeAsync<UserViewModel>(contentStream);

            return user;
        }

        public async Task<UserViewModel> DeleteFriend(string userId)
        {
            var httpResponseMessage = await _httpClient.DeleteAsync(string.Format(RoutesAPI.DeleteFriend, userId));
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            var user = await JsonSerializer.DeserializeAsync<UserViewModel>(contentStream);

            return user;
        }

        public async Task<UserViewModel> BlockUser(string userId)
        {
            var httpResponseMessage = await _httpClient.PostAsJsonAsync(RoutesAPI.BlockUser, userId);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            var user = await JsonSerializer.DeserializeAsync<UserViewModel>(contentStream);

            return user;
        }

        public async Task<UserViewModel> UnblockUser(string userId)
        {
            var httpResponseMessage = await _httpClient.DeleteAsync(string.Format(RoutesAPI.UnblockUser, userId));
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            var user = await JsonSerializer.DeserializeAsync<UserViewModel>(contentStream);

            return user;
        }

        public async Task<UserViewModel> UpdateUser(UserUpdateModel userModel)
        {
            if (string.IsNullOrEmpty(userModel.UserName) || string.IsNullOrEmpty(userModel.Email))
            {
                throw new ArgumentNullException("User name or email cannot be null");
            }

            using var content = new MultipartFormDataContent();
            content.Add(new StringContent(userModel.UserName), "UserName");
            content.Add(new StringContent(userModel.Email), "Email");
            content.Add(new StringContent(userModel.Id), "Id");

            if (userModel.File is not null)
            {
                var file = userModel.File;
                var fileStream = new StreamContent(file.OpenReadStream());
                fileStream.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                content.Add(fileStream, "File", file.FileName);
            }

            var httpResponseMessage = await _httpClient.PutAsync(RoutesAPI.UpdateUser, content);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            var updatedUser = await JsonSerializer.DeserializeAsync<UserViewModel>(contentStream);

            return updatedUser;
        }

        public async void ChangePassword(UserChangePasswordModel userChangePasswordModel)
        {
            var httpResponseMessage = await _httpClient.PostAsJsonAsync(RoutesAPI.ChangePassword, userChangePasswordModel);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
        }
    }
}
