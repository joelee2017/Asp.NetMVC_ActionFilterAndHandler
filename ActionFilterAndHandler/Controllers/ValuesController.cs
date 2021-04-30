using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ActionFilterAndHandler.Controllers
{
    public class ValuesController : ApiController
    {
        // GET: api/Values
        public IEnumerable<string> Get()
        {
            throw new NotImplementedException();
            return new string[] { "value1", "value2" };
        }
    }
}
