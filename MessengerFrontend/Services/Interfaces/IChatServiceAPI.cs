using MessengerFrontend.Models.Chats;

namespace MessengerFrontend.Services.Interfaces
{
    public interface IChatServiceAPI
    {
        public Task<IEnumerable<ChatViewModel>> GetAllChatrooms();
        public Task<ChatViewModel> GetChatroom(int id);
    }
}
