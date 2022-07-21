using MessengerFrontend.Models.ActionLogs;
using MessengerFrontend.Models.Users;
using MessengerFrontend.Routes;
using MessengerFrontend.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MessengerFrontend.Services
{
    public class ActionLogServiceAPI : BaseServiceAPI, IActionLogServiceAPI
    {
        public ActionLogServiceAPI(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, httpContextAccessor)
        { }

        public async Task<IEnumerable<ActionLogViewModel>> GetAllLogs(DateTime? date = null)
        {
            var httpResponseMessage = await _httpClient.GetAsync(RoutesAPI.GetAllLogs);
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var logs = await JsonSerializer.DeserializeAsync<IEnumerable<ActionLogViewModel>>(contentStream);

            return logs;
        }
    }
}
