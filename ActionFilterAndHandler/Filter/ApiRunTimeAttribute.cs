using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ActionFilterAndHandler.Filter
{
    public class ApiRunTimeAttribute : ActionFilterAttribute
    {
        private readonly Stopwatch _sw = new Stopwatch();
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            _sw.Start();
        }


        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            string httpMethod = actionExecutedContext.Request.Method.ToString();
            string controllerName = actionExecutedContext.ActionContext
                                                        .ControllerContext
                                                        .ControllerDescriptor
                                                        .ControllerName;
            string actionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;

            _sw.Stop();
            TimeSpan ts = _sw.Elapsed;
            _sw.Reset();

            // output any where
            // demo 1
            string timerLog = $"API Timer: *{ts.ToString()}* from *{httpMethod} /{controllerName}/{actionName}*";
            System.Diagnostics.Debug.WriteLine(timerLog);
            // demo 2
            actionExecutedContext.Response.Headers.Add("X-API-Timer", ts.ToString());
        }

    }
}