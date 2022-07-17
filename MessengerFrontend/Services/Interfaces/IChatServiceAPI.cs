using MessengerFrontend.Models.Chats;
using MessengerFrontend.Models.UserAccounts;

namespace MessengerFrontend.Services.Interfaces
{
    public interface IChatServiceAPI
    {
        public Task<IEnumerable<ChatViewModel>> GetAllChatrooms(string token);
        public Task<ChatViewModel> GetChatroom(int id, string token);
        public Task<ChatViewModel> CreateChatroom(ChatCreateModel model, string token);
        public Task<ChatViewModel> EditChatroom(ChatUpdateModel model, string token);
        public Task<bool> DeleteChatroom(int chatId, string token);
        public Task<UserAccountViewModel> AddToChatroom(ChatInviteModel model, string token);
        public Task<IEnumerable<UserAccountViewModel>> GetAllMembers(int id, string token);
        public Task<UserAccountViewModel> GetCurrentUserAccount(int chatId, string token);
        public Task<UserAccountViewModel> SetAdmin(int userAccountId, string token);
        public Task<UserAccountViewModel> UnsetAdmin(int userAccountId, string token);
        public Task<UserAccountViewModel> MuteUser(int userAccountId, string token);
        public Task<UserAccountViewModel> UnmuteUser(int userAccountId, string token);
        public Task<bool> KickUser(int userAccountId, string token);
        public Task<bool> LeaveChat(int chatId, string token);
    }
}
