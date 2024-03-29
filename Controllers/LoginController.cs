﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Pedalacom.BLogic.Authentication;

namespace Pedalacom.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public LoginController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("/login")]
        public IActionResult Login(User user)
        {
            var result = _authenticationService.AuthenticateAsync(HttpContext, "BasicAuthentication").Result;

            if (result.Succeeded)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest("Login failed");
            }
        }
    }




    public class User
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}

