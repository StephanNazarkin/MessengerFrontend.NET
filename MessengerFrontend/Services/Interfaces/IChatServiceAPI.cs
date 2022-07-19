using MessengerFrontend.Models.Chats;
using MessengerFrontend.Models.UserAccounts;

namespace MessengerFrontend.Services.Interfaces
{
    public interface IChatServiceAPI
    {
        public Task<IEnumerable<ChatViewModel>> GetAllChatrooms();
        public Task<ChatViewModel> GetChatroom(int id);
        public Task<ChatViewModel> CreateChatroom(ChatCreateModel model);
        public Task<ChatViewModel> EditChatroom(ChatUpdateModel model);
        public Task<bool> DeleteChatroom(int chatId);
        public Task<UserAccountViewModel> AddToChatroom(ChatInviteModel model);
        public Task<IEnumerable<UserAccountViewModel>> GetAllMembers(int id);
        public Task<UserAccountViewModel> GetCurrentUserAccount(int chatId);
        public Task<UserAccountViewModel> SetAdmin(int userAccountId);
        public Task<UserAccountViewModel> UnsetAdmin(int userAccountId);
        public Task<UserAccountViewModel> MuteUser(int userAccountId);
        public Task<UserAccountViewModel> UnmuteUser(int userAccountId);
        public Task<bool> KickUser(int userAccountId);
        public Task<bool> LeaveChat(int chatId);
    }
}
