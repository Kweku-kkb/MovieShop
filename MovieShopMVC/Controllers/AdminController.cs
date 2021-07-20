
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> CreateMovie()
        {
            return View();
        }
    }
}