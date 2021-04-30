using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ActionFilterAndHandler.Interface;
using ActionFilterAndHandler.Tokens;

namespace ActionFilterAndHandler.Controllers
{
    public class TokenController : ApiController
    {
        public IHttpActionResult Post([FromBody]LoginModel loginModel)
        {
            // Hack: 應該與資料庫做比對
            if (loginModel.Name == "kkbruce" && loginModel.Password == "skilltree")
            {
                var secretKey = "BAD6809DCB5AFBAAA9DC8CABB4F4AB3D7DCA2438A721B3686B6B0D3288239D00";
                var payload = new
                {
                    iss = "BruceChen",
                    iat = DateTimeOffset.Now.ToUnixTimeSeconds(),
                    exp = DateTimeOffset.Now.AddSeconds(6000).ToUnixTimeSeconds(),
                    aud = "kkbruce.tw",
                    sub = "i@kkbruce.tw",
                    name = loginModel.Name,
                    hash = "13B90F95960A105561E292C1640346BD3C178C5948545284875E5275BB8E100C"
                };

                string token = JWT.JsonWebToken.Encode(payload, secretKey, JWT.JwtHashAlgorithm.HS256);

                return Ok($"token={token}");
            }
            else
            {
                return Unauthorized();
            }

        }

        public class LoginModel
        {
            public string Name { get; set; }
            public string Password { get; set; }
        }

        private readonly IToken _token;

        /// <summary>
        /// POC
        /// </summary>
        public TokenController()
        {
            _token = new FileToken();
        }

        /// <summary>
        /// 正常應使用 IoC
        /// </summary>
        /// <param name="token"></param>
        public TokenController(IToken token)
        {
            _token = token;
        }

        public IHttpActionResult Get(string appId, string appSecret)
        {
            if (string.IsNullOrWhiteSpace(appId) ||
                string.IsNullOrWhiteSpace(appSecret))
            {
                return BadRequest("Error: Parameter is incorrect.");
            }

            // 進行驗證
            string hashCode = _token.Validation(appId, appSecret);
            if (hashCode.Contains("Error"))
            {
                return BadRequest("Error: Parameter is incorrect.");
            }

            // TODO: Check Token From Redis / DB
            // POC Read from File
            #region POC Code - FileReader
            string fileName = "AccessToken.txt";
            string folder = HttpContext.Current.Server.MapPath("~/App_Data/");
            string fullPath = folder + fileName;
            System.IO.Directory.CreateDirectory(folder);
            if (System.IO.File.Exists(fullPath))
            {
                string fileToken = System.IO.File.ReadAllText(fullPath);
                if (!string.IsNullOrWhiteSpace(fileToken))
                {
                    return Ok(fileToken);
                }
            }
            #endregion

            // 產生 Token
            string rfcToken = UrlBase64.ToUrlReplace(Crypto.Rfc2898(hashCode));

            // TODO: Add Token to Redis / DB
            // POC write to File
            #region POC Code - FileWriter
            System.IO.File.WriteAllText(fullPath, rfcToken);
            #endregion

            return Ok(rfcToken);
        }

    }
}
