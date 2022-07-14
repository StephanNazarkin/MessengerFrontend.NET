using MessengerFrontend.Models.Users;

namespace MessengerFrontend.Services.Interfaces
{
    public interface IAccountServiceAPI
    {
        public Task<UserViewModel> Login(UserLoginModel model);
        public Task<UserViewModel> Register(UserViewModel model);
        public Task<IEnumerable<UserViewModel>> GetAllFriends();
        public Task<IEnumerable<UserViewModel>> GetAllBlockedUsers();
        public Task<UserViewModel> GetCurrentUser();
    }
}
