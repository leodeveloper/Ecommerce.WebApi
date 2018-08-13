using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Web.Model;
using Ecommerce.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _iProductRepository;

        public ProductController(IProductRepository iProductRepository)
        {
            _iProductRepository = iProductRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IList<ProductViewModel> productViewModel = _iProductRepository.GetProducts();
            return View(productViewModel);
        }

        [HttpGet]
        public IActionResult ProductDetail(int productId)
        {
            ProductViewModel productViewModel = _iProductRepository.GetProductById(productId);
            return View(productViewModel);
        }
    }
}