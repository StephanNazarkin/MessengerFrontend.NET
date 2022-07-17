using MessengerFrontend.Models.Users;

namespace MessengerFrontend.Services.Interfaces
{
    public interface IAccountServiceAPI
    {
        public Task<UserViewModel> Login(UserLoginModel model);
        public Task<UserViewModel> Register(UserViewModel model);
        public Task<IEnumerable<UserViewModel>> GetAllFriends(string token);
        public Task<IEnumerable<UserViewModel>> GetAllBlockedUsers(string token);
        public Task<UserViewModel> GetCurrentUser(string token);
        public Task<UserViewModel> GetUserByUserName(string userName, string token);
        public Task<IEnumerable<UserViewModel>> GetAllUsers(string token);
        public Task<UserViewModel> AddFriend(string userId, string token);
        public Task<UserViewModel> DeleteFriend(string userId, string token);
        public Task<UserViewModel> BlockUser(string userId, string token);
        public Task<UserViewModel> UnblockUser(string userId, string token);
        public Task<UserViewModel> UpdateUser(UserUpdateModel userModel, string token);
    }
}
