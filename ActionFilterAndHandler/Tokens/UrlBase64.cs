using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActionFilterAndHandler.Tokens
{
    public class UrlBase64
    {
        /// <summary>
        /// 將 +、/ 符號做替換
        /// </summary>
        /// <param name="base64String"></param>
        /// <returns></returns>
        public static string ToUrlReplace(string base64String)
        {
            return base64String.Replace("+", "-").Replace("/", "_");
        }

        /// <summary>
        /// 還原 +、/ 符號
        /// </summary>
        /// <param name="base64String"></param>
        /// <returns></returns>
        public static string FromUrlReplace(string base64String)
        {
            return base64String.Replace("-", "+").Replace("_", "/");
        }
    }
}