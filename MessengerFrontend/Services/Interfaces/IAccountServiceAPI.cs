using MessengerFrontend.Models.Users;

namespace MessengerFrontend.Services.Interfaces
{
    public interface IAccountServiceAPI
    {
        public Task<UserViewModel> GetCurrentUser();
    }
}
