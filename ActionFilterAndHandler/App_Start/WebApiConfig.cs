using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using ActionFilterAndHandler.Filter;
using ActionFilterAndHandler.Formatter;
using ActionFilterAndHandler.Handler;

namespace ActionFilterAndHandler
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 設定和服務      
            //config.MessageHandlers.Add(new TokenHandler());
            //Key 應該是動態，範例寫死 SkillTree
            config.MessageHandlers.Add(new ApiKeyHandler("SkillTree"));
            //config.MessageHandlers.Add(new DirectlyResponseHandler());
            config.MessageHandlers.Add(new DebugWriteHandler());

            config.EnableSystemDiagnosticsTracing();
            config.Filters.Add(new ApiVersionAttribute());
            config.Filters.Add(new ApiRunTimeAttribute());
            config.Filters.Add(new ElmahErrorAttribute());
            //config.Formatters.Remove(config.Formatters.JsonFormatter);
            //config.Formatters.Remove(config.Formatters.XmlFormatter);
            //config.Formatters.Remove(new XmlMediaTypeFormatter());

            config.Formatters.Add(new ProductCsvFormatter());

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //只要找不到的都透過以下路由都直接給 404
            //config.Routes.MapHttpRoute(
            //     name: "NotFound",
            //     routeTemplate: "{*path}",
            //     defaults: new { controller = "Error404", action = "NotFound" }
            //);

            // pre-route message handler
            config.Routes.MapHttpRoute(
                name: "HandlertApi",
                routeTemplate: "api/admin/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: null,
                handler: new ApiKeyHandler("SkillTree")
            );
            // global message handler
            config.MessageHandlers.Add(new LoggerHandler());

        }
    }
}
