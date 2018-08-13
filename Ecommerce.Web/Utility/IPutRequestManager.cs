using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Web.Utility
{
    public interface IPutRequestManager
    {
        string SendPutRequest(string uri, string content, string login, string password, bool allowAutoRedirect, string contentType, HeadersDto headers);
    }
}
