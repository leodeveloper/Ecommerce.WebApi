using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Model.Dto;
using Ecommerce.Model.EntityFrameWork;
using Ecommerce.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ecommerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {

        private readonly IProductService _iProductService;

        public ProductController(IProductService iProductService)
        {
            _iProductService = iProductService;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var products = await _iProductService.GetProductsAsync();
                return Ok(products);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> Get(int productId)
        {
            try
            {
                var product = await _iProductService.GetProductAsync(productId);

                if (product == null)
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
