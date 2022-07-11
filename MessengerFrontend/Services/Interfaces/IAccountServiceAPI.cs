using MessengerFrontend.Models.Users;

namespace MessengerFrontend.Services.Interfaces
{
    public interface IAccountServiceAPI
    {
        public Task<IEnumerable<UserViewModel>> GetAllFriends();
        public Task<IEnumerable<UserViewModel>> GetAllBlockedUsers();
        public Task<UserViewModel> GetCurrentUser();
    }
}
