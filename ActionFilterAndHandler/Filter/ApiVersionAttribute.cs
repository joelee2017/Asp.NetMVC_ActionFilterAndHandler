using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http.Filters;

namespace ActionFilterAndHandler.Filter
{
    public class ApiVersionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {

            string assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            actionExecutedContext.Response.Headers.Add("X-API-Version", assemblyVersion);
        }
    }
}