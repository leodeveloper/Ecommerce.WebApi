using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ecommerce.WebApi.Client.Helper
{
    public class HelperEnum
    {
        public enum RequestMethod
        {
            Get,
            Post,
            Put,
            Delete
        };

        public enum HttpRequestContentType
        {
            [Description("application/json")]
            ApplicationJson,

        }
    }
}
