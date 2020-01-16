using System;
using library_api.Models;
using library_api.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace library_api.Controllers
{
    public class UserController : Controller
    {
        private static Global _global;
        public UserController(Global global)
        {
            _global = global;
        }

        [Authorization]
        [Route("users")]
        [HttpGet]
        public IActionResult List()
        {
            try
            {
                User user = new User();
                return Ok(user.List());
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Authorization]
        [Route("user/{userID}")]
        [HttpGet]
        public IActionResult Get(int userID)
        {
            try
            {
                User user = new User();
                return Ok(user.Get(userID));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Route("user")]
        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            try
            {
                User newUser = user.Create();
                string path = "Templates/Emails/EmailConfirmation.html";
                user.SendEmailConfirmation(newUser, path, _global.BaseUrl);
                return Ok(user);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Authorization]
        [Route("user/{userID}")]
        [HttpPut]
        public IActionResult Update(int userID, [FromBody] User user)
        {
            try
            {
                user.Id = userID;
                return Ok(user.Update());
            } catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Authorization]
        [Route("user/{userID}")]
        [HttpDelete]
        public IActionResult Delete(int userID)
        {
            try
            {
                User user = new User
                {
                    Id = userID
                };
                return Ok(user.Delete());
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        [Authorization]
        [Route("user/filter")]
        [HttpGet]
        public IActionResult Filter()
        {
            try
            {
                var filters = HttpContext.Request.Query;
                User user = new User();
                return Ok(user.Filter(filters));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Authorization]
        [Route("user/email-confirmation/{accessKey}")]
        [HttpGet]
        public IActionResult EmailConfirmation(string accessKey)
        {
            try
            {
                User user = new User();
                return Ok(user.ConfirmUserEmail(accessKey));
            } catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
