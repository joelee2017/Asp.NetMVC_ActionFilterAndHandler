using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionFilterAndHandler.Interface
{
    public interface IToken
    {
        string Validation(string appId, string appSecret);
    }
}
