using MessengerFrontend.Routes;
using System.Net.Http.Headers;

namespace MessengerFrontend.Services
{
    public class BaseServiceAPI
    {
        protected readonly HttpClient _httpClient;

        public BaseServiceAPI(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClientFactory.CreateClient(RoutesAPI.ApiName);
            var token = httpContextAccessor.HttpContext.Session.GetString("Token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(RoutesAPI.TokenHeader, token);
        }
    }
}
