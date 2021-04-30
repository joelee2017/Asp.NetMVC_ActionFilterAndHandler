using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActionFilterAndHandler.Interface;

namespace ActionFilterAndHandler.Tokens
{
    public class FileToken : IToken
    {
        public string Validation(string appId, string appSecret)
        {
            // TODO: 驗證 Id 與 Secret
            // POC - 寫死
            if (appId == "kkbruce" && appSecret == "skilltree")
            {
                return Guid.NewGuid().ToString();
            }

            return "Error: Parameter is incorrect.";
        }
    }
}