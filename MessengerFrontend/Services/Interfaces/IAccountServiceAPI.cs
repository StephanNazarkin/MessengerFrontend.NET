using MessengerFrontend.Models.Users;

namespace MessengerFrontend.Services.Interfaces
{
    public interface IAccountServiceAPI
    {
        public Task<IEnumerable<UserViewModel>> GetAllFriends();
    }
}
