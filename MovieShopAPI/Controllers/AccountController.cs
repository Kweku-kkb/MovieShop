﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestModel model)
        {
            var user = await _userService.Login(model.Email, model.Password);
            if (user == null)
            {
                return Unauthorized("Password or Email is invalid");
            }
            return Ok("You passed");
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequestModel model)
        {
            var createdUser = await _userService.RegisterUser(model);

            // send the URL for newly created user also
            // 5000

            return CreatedAtRoute("GetUser", new { id = createdUser.Id }, createdUser);

            // 201
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetUser")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);

            if (user == null)
            {
                return NotFound($"User does not exists for {id}");
            }

            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            if (users == null)
            {
                return NotFound($"No users found");
            }
            return Ok(users);
        }
    }
}