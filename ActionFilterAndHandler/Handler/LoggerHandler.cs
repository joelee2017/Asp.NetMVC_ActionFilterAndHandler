using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ActionFilterAndHandler.Handler
{
    public class LoggerHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Logger(request);
            return base.SendAsync(request, cancellationToken);
        }

        /// <summary>
        /// 日誌記錄
        /// </summary>
        /// <param name="request">請求</param>
        private void Logger(HttpRequestMessage request)
        {
            var info = new LogInfo
            {
                HttpMethod = request.Method.Method,
                UriAccessed = request.RequestUri.AbsoluteUri,
                IPAddress = HttpContext.Current != null ? HttpContext.Current.Request.UserHostAddress : "0.0.0.0",
            };

            if (request.Content != null)
            {
                // 讀取 Body
                request.Content.ReadAsByteArrayAsync()
                    .ContinueWith((task) =>
                    {
                        info.BodyContent = Encoding.UTF8.GetString(task.Result);
                    });
            }

            // 序列化與儲存
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(info);
            string uniqueid = DateTime.Now.Ticks.ToString();
            string logfile = $"E:\\Temp\\{uniqueid}.txt";
            System.IO.File.WriteAllText(logfile, json);
        }

        /// <summary>
        /// 日誌物件
        /// </summary>
        public class LogInfo
        {
            //public List<string> Header { get; set; }
            public string HttpMethod { get; set; }
            public string UriAccessed { get; set; }
            public string IPAddress { get; set; }
            public string BodyContent { get; set; }
        }
    }
}