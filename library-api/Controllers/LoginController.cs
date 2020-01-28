using System;
using library_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace library_api.Controllers
{
    public class LoginController : Controller
    {
        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] Login credentials)
        {
            try
            {
                User user = new Login().CheckLogin(credentials);
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
    }
}
