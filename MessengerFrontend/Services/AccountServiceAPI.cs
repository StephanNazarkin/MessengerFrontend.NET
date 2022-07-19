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
            if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                throw new RegistrationException("You caused a registration error!!!");
            }
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
            if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                throw new LoadUsersException("Sorry, we can't load this users. It's most likely a server or connection issue.");
            }
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            var friends = await JsonSerializer.DeserializeAsync
                <IEnumerable<UserViewModel>>(contentStream);

            return friends;

        }

        public async Task<IEnumerable<UserViewModel>> GetAllBlockedUsers()
        {
            var httpResponseMessage = await _httpClient.GetAsync(RoutesAPI.GetAllBlockedUsers);
            if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                throw new LoadUsersException("Sorry, we can't load this users. It's most likely a server or connection issue.");
            }
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            var blockedUsers = await JsonSerializer.DeserializeAsync
                <IEnumerable<UserViewModel>>(contentStream);

            return blockedUsers;
        }
        
        public async Task<UserViewModel> GetCurrentUser()
        {
            var httpResponseMessage = await _httpClient.GetAsync(RoutesAPI.GetCurrentUser);
            if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                throw new CurrentUserException("Sorry, we can't load your current account. It's most likely a server or connection issue.");
            }
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
            if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                throw new LoadUsersException("Sorry, we can't load this users. It's most likely a server or connection issue.");
            }
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            var allUsers = await JsonSerializer.DeserializeAsync
                <IEnumerable<UserViewModel>>(contentStream);

            return allUsers;
        }

        public async Task<UserViewModel> AddFriend(string userId)
        {
            var httpResponseMessage = await _httpClient.PostAsJsonAsync(RoutesAPI.AddFriend, userId);
            if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                throw new FriendUserException("Something went wrong, when you tried to add this user to your friend list.");
            }
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            var user = await JsonSerializer.DeserializeAsync<UserViewModel>(contentStream);

            return user;
        }

        public async Task<UserViewModel> DeleteFriend(string userId)
        {
            var httpResponseMessage = await _httpClient.DeleteAsync(string.Format(RoutesAPI.DeleteFriend, userId));
            if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                throw new FriendUserException("Something went wrong, when you tried to delete this user from your friend list.");
            }
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            var user = await JsonSerializer.DeserializeAsync<UserViewModel>(contentStream);

            return user;
        }

        public async Task<UserViewModel> BlockUser(string userId)
        {
            var httpResponseMessage = await _httpClient.PostAsJsonAsync(RoutesAPI.BlockUser, userId);
            if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                throw new BlockedUserException("Something went wrong, when you tried to add this user to your black list.");
            }
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            var user = await JsonSerializer.DeserializeAsync<UserViewModel>(contentStream);

            return user;
        }

        public async Task<UserViewModel> UnblockUser(string userId)
        {
            var httpResponseMessage = await _httpClient.DeleteAsync(string.Format(RoutesAPI.UnblockUser, userId));
            if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                throw new BlockedUserException("Something went wrong, when you tried to delete this user from your black list.");
            }
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            var user = await JsonSerializer.DeserializeAsync<UserViewModel>(contentStream);

            return user;
        }

        public async Task<UserViewModel> UpdateUser(UserUpdateModel userModel)
        {
            if (string.IsNullOrEmpty(userModel.UserName) || string.IsNullOrEmpty(userModel.Email))
            {
                throw new CurrentUserException("User name or email cannot be null");
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
            if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                throw new CurrentUserException("Something went wrong, when you tried to update your account profile.");
            }
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            var updatedUser = await JsonSerializer.DeserializeAsync<UserViewModel>(contentStream);

            return updatedUser;
        }

        public async void ChangePassword(UserChangePasswordModel userChangePasswordModel)
        {
            var httpResponseMessage = await _httpClient.PostAsJsonAsync(RoutesAPI.ChangePassword, userChangePasswordModel);
            if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                throw new CurrentUserException("Something went wrong, when you tried to change your password.");
            }
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
        }
    }
}
