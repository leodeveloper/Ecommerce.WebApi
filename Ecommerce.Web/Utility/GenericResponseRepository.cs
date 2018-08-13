using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Web.Utility
{
    public class GenericResponseRepository<T> where T : class
    {
        /// <summary>
        /// Json string to T object
        /// </summary>
        /// <param name="apiResponse">string of json</param>
        public T Convert(string apiResponse)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            return JsonConvert.DeserializeObject<T>(apiResponse, settings);

        }
        public List<T> Convert_to_List(string apiResponse)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            return JsonConvert.DeserializeObject<List<T>>(apiResponse, settings);

        }
    }
}
