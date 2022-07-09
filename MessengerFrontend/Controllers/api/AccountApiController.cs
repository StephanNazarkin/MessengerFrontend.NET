using Microsoft.AspNetCore.Mvc;
using System.Web.Http;

namespace MessengerFrontend.Controllers.api
{
    public class AccountApiController : ApiController
    {
        public IHttpActionResult Index()
        {
            return Ok();
        }
    }
}
