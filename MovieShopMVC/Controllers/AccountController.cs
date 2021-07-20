using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieShopMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        // method for registration

        // show the empty view that wil have all the text boxes and submit button
        // localhost/account/register

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestModel model)
        {


            if (!ModelState.IsValid)
            {
                return View();
            }

            var craetedUser = await _userService.RegisterUser(model);
            // redirect to Login

            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userService.Login(model.Email, model.Password);

            if (user == null)
            {
                // wrong password
                ModelState.AddModelError(string.Empty, "Invalid password");
                return View();
            }

            // correct pasword
            // display, FirstName, LastName, Email
            // Button/Link Logout
            // Cookie Based Authentication....
            return View();
        }

    }
}