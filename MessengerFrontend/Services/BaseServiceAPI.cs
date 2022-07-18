using MessengerFrontend.Routes;

namespace MessengerFrontend.Services
{
    public class BaseServiceAPI
    {
        protected readonly HttpClient _httpClient;

        public BaseServiceAPI(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(RoutesAPI.GetAllChatrooms);
        }
    }
}
