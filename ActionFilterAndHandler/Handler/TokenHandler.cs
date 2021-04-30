using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ActionFilterAndHandler.Handler
{
    public class TokenHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                         CancellationToken cancellationToken)
        {
            // 僅 /api/Token 不需檢查 Token
            if ((request.Method == HttpMethod.Get ||
                request.Method == HttpMethod.Post) &&
                (request.RequestUri.AbsolutePath == "/api/Token" ||
                 request.RequestUri.AbsolutePath == "/api/token"))
            {
                return base.SendAsync(request, cancellationToken);
            }
            else
            {
                // 檢查 Token
                if (!ParseToken(request))
                {
                    return NoTokenResponse();
                }
                return base.SendAsync(request, cancellationToken);
            }
        }

        /// <summary>
        /// 無 Token 的回應訊息
        /// </summary>
        private static Task<HttpResponseMessage> NoTokenResponse()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("Error: No API Token!")
            };

            return Task.FromResult(response);
        }

        /// <summary>
        /// 解析 Token 是否正確；
        /// POC 僅做取某一 Header 比對
        /// </summary>
        private bool ParseToken(HttpRequestMessage message)
        {
            string token;
            try
            {
                // 未含 Header 會爆 Exception
                // Header 名稱可自訂，雙方溝通好即可
                IEnumerable<string> headerValues = message.Headers.GetValues("AccessToken");
                token = headerValues.FirstOrDefault();

                // 也可採用 JWT Lab 裡的標準方式，這裡展示另一種處理方式
            }
            catch (Exception)
            {
                return false;
            }

            #region POC Code - FileReader
            string fileName = "AccessToken.txt";
            string folder = HttpContext.Current.Server.MapPath("~/App_Data/");
            string fullPath = folder + fileName;
            if (System.IO.File.Exists(fullPath))
            {
                string fileToken = System.IO.File.ReadAllText(fullPath);
                if (!string.IsNullOrWhiteSpace(fileToken))
                {
                    return token == fileToken;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            #endregion
        }
    }
}