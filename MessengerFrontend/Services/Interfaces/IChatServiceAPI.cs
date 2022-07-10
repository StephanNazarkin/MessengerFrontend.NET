using MessengerFrontend.Models.Chat;

namespace MessengerFrontend.Services.Interfaces
{
    public interface IChatServiceAPI
    {
        public Task<IEnumerable<ChatViewModel>> GetAllChatrooms();
    }
}
