using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Model.Dto
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desciption { get; set; }
        public decimal Price { get; set; }
        public string Photo { get; set; }
    }
}
