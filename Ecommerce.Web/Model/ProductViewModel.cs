using Ecommerce.Model.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Web.Model
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal Price { get; set; }
        public string Photo { get; set; }

        public string Desciption { get; set; }
    }


   
    
}
