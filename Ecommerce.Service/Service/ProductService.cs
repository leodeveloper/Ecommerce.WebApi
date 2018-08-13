using Ecommerce.Model.Dto;
using Ecommerce.Model.EntityFrameWork;
using Ecommerce.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Service
{
    public class ProductService : IProductService
    {
        private readonly EnityFramWorkDbContext _context;
        public ProductService(EnityFramWorkDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get All List of Products from Product Table
        /// </summary>
        /// <returns>return list of products</returns>
        public async Task<IList<Product>> GetProductsAsync()
        {
            var products = await _context.Products.ToArrayAsync();
            return products;
        }

        /// <summary>
        /// Get Product from product by productId
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>return single product</returns>
        public async Task<Product> GetProductAsync(int productId)
        {
            var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == productId);
            return product;
        }
    }
}
