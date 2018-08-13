using Ecommerce.WebApi.Client.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.WebApi.Client.Interface
{
    public interface IGetRequestManager
    {
        string SendRequest(string uri, string login, string password, bool allowAutoRedirect, string contentType, HeadersDto headers);
    }
}
