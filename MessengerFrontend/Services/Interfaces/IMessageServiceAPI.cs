using MessengerFrontend.Models.Messages;

namespace MessengerFrontend.Services.Interfaces
{
    public interface IMessageServiceAPI
    {
        public Task<MessageViewModel> GetMessage(int messageId, string token);
        public Task<IEnumerable<MessageViewModel>> GetMessagesFromChat(int chatId, string token);
        public Task<bool> SendMessage(MessageCreateModel model, string token);
        public Task<MessageViewModel> EditMessage(MessageUpdateModel model, string token);
        public Task<bool> DeleteMessage(int id, string token);
    }
}
