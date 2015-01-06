using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace PollApi
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ValidModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ActionArguments.ContainsValue(null))
            {
                actionContext.Response = 
                    actionContext.Request.CreateErrorResponse(
                        HttpStatusCode.BadRequest, "The argument cannot be null");
            }
        }
    }
}