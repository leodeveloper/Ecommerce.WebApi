using Ecommerce.WebApi.Client.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.WebApi.Client.Interface
{
    public interface IPutRequestManager
    {
        string SendRequest(string uri, string content, string login, string password, bool allowAutoRedirect, string contentType, HeadersDto headers);
    }
}
