﻿using System;
using library_api.Entities;
using library_api.Interfaces;
using library_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace library_api.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogin login;

        public LoginController(ILogin _login)
        {
            login = _login;
        }

        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] Login credentials)
        {
            try
            {
                User user = login.CheckLogin(credentials);
                if (user != null)
                {
                    return Ok(user);
                }

                return Ok();
            } catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Route("renew-session/{accessKey}")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult RenewSession(string accessKey) {
            try {
                User user = login.RenewSession(accessKey);
                if (user != null) 
                    return Ok(user);
                    
                return NotFound("Invalid access key");
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }
    }
}
