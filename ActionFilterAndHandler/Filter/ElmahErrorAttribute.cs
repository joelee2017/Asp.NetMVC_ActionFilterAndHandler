using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace ActionFilterAndHandler.Filter
{
    public class ElmahErrorAttribute :ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception != null)
                Elmah.ErrorSignal
                       .FromCurrentContext()
                       .Raise(actionExecutedContext.Exception);
            base.OnException(actionExecutedContext);
        }
    }
}