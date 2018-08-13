using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Web.Utility
{
    public interface IPostRequestManager
    {
        string SendPostRequest(string uri, string content, string login, string password, bool allowAutoRedirect, string contentType, HeadersDto headers);
    }
}
