using MessengerFrontend.Models.Messages;

namespace MessengerFrontend.Services.Interfaces
{
    public interface IMessageServiceAPI
    {
        public Task<IEnumerable<MessageViewModel>> GetMessagesFromChat(int chatId);
        public Task<MessageViewModel> SendMessage(MessageCreateModel model);
    }
}
