using Ecommerce.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Web.Model
{
    public class BasketItemViewModel
    {
        public int Id { get; set; }
        public ProductViewModel Product { get; set; }
        public int Quantity { get; set; }
    }   
   
}
