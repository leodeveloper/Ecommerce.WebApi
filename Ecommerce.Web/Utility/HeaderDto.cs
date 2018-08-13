using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Web.Utility
{
    public class HeaderDto
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class HeadersDto
    {
        public HeadersDto()
        {
            this.Headers = new List<HeaderDto>();
        }

        public IList<HeaderDto> Headers { get; set; }
    }

    public class Conversion_HeaderDto
    {
        public static HeadersDto Values_To_HeaderDto(string name, string value)
        {
            HeadersDto headersDto = new HeadersDto();
            headersDto.Headers.Add(new HeaderDto { Name = name, Value = value });
            return headersDto;
        }
    }
}
