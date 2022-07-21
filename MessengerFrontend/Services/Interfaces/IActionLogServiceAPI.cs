using MessengerFrontend.Models.ActionLogs;

namespace MessengerFrontend.Services.Interfaces
{
    public interface IActionLogServiceAPI
    {
        public Task<IEnumerable<ActionLogViewModel>> GetAllLogs(DateTime? date = null);
    }
}
