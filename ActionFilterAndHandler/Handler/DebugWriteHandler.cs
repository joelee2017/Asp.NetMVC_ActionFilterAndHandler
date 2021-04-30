using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ActionFilterAndHandler.Handler
{
    public class DebugWriteHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Debug.WriteLine("DebugWriteHandler - 處理請求");
            // 呼叫 inner handler
            return base.SendAsync(request, cancellationToken);
            Debug.WriteLine("DebugWriteHandler - 處理回應");
        }
    }
}