using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet]
        public IActionResult Index(int statusCode)
        {
            return View(statusCode);
        }       
    }
}