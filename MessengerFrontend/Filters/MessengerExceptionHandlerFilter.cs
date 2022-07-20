using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MessengerFrontend.Filters
{
    public class MessengerExceptionHandlerFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            var actionName = filterContext.RouteData.Values["action"];
            var exceptionMessage = filterContext.Exception.Message;
            filterContext.Result = new RedirectToActionResult("Exception", "Error", new { actionName = actionName, errorMessage = exceptionMessage });
            /*            context.Result = new ContentResult
                        {
                            Content = $"{actionName} caused that exception: \n {exceptionMessage}"
                        };*/
            filterContext.ExceptionHandled = true;
        }
    }
}
