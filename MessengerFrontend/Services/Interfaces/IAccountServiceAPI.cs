using MessengerFrontend.Models.Users;

namespace MessengerFrontend.Services.Interfaces
{
    public interface IAccountServiceAPI
    {
        public Task<UserViewModel> Login(UserViewModel model);
        public Task<UserViewModel> Register(UserViewModel model);
        public Task<IEnumerable<UserViewModel>> GetAllFriends();
        public Task<IEnumerable<UserViewModel>> GetAllBlockedUsers();
        public Task<UserViewModel> GetCurrentUser();
        public Task<UserViewModel> GetUserByUserName(string userName);
        public Task<IEnumerable<UserViewModel>> GetAllUsers();
        public Task<UserViewModel> AddFriend(string userId);
        public Task<UserViewModel> DeleteFriend(string userId);
        public Task<UserViewModel> BlockUser(string userId);
        public Task<UserViewModel> UnblockUser(string userId);
        public Task<UserViewModel> UpdateUser(UserUpdateModel userModel);
        public void ChangePassword(UserChangePasswordModel userModel);
    }
}
