using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Web.Utility
{
    public interface IGetRequestManager
    {
        string SendGetRequest(string uri, string login, string password, bool allowAutoRedirect, string contentType, HeadersDto headers);
    }
}
