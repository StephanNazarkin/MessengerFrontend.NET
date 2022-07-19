using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MessengerFrontend.Filters
{
    public class MessengerExceptionHandlerFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            string actionName = context.ActionDescriptor.DisplayName;
            string exceptionStack = context.Exception.StackTrace;
            string exceptionMessage = context.Exception.Message;
            context.Result = new ContentResult
            {
                Content = $"{actionName} caused that exception: \n {exceptionMessage}"
            };
            context.ExceptionHandled = true;
        }
    }
}
