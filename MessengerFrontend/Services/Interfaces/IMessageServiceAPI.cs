using MessengerFrontend.Models.Messages;

namespace MessengerFrontend.Services.Interfaces
{
    public interface IMessageServiceAPI
    {
        public Task<MessageViewModel> GetMessage(int messageId);
        public Task<IEnumerable<MessageViewModel>> GetMessagesFromChat(int chatId);
        public Task<bool> SendMessage(MessageCreateModel model);
        public Task<bool> SendAdminsMessage(MessageCreateModel model);
        public Task<MessageViewModel> EditMessage(MessageUpdateModel model);
        public Task<bool> DeleteMessage(int id);
    }
}
