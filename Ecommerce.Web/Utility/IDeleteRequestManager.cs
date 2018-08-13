using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Web.Utility
{
    public interface IDeleteRequestManager
    {
        string SendDeleteRequest(string uri, string content, string login, string password, bool allowAutoRedirect, string contentType, HeadersDto headers);
    }
}
