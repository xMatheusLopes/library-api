using library_api.Interfaces;
using library_api.Entities;
using library_api.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace library_api.Controllers
{
    public class UserController : Controller
    {
        private static Global _global;
        private readonly IUser _user;

        public UserController(Global global, IUser user)
        {
            _global = global;
            _user = user;
        }

        [Route("users")]
        [HttpGet]
        public IActionResult List()
        {
            try
            {
                return Ok(_user.List());
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [Route("user/{userID}")]
        [HttpGet]
        public IActionResult Get(int userID)
        {
            try
            {
                return Ok(_user.Get(userID));
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [Route("user")]
        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            try
            {
                User newUser = _user.Create(user);
                string path = "Templates/Emails/EmailConfirmation.html";
                _user.SendEmailConfirmation(newUser, path, _global.BaseUrl);
                return Ok(newUser);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [Route("user/{userID}")]
        [HttpPut]
        public IActionResult Update(int userID, [FromBody] User user)
        {
            try
            {
                user.Id = userID;
                return Ok(_user.Update(user));
            } catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [Route("user/{userID}")]
        [HttpDelete]
        public IActionResult Delete(int userID)
        {
            try
            {
                User user = _user.Get(userID);
                return Ok(user.Delete(user));
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }

        }

        [Route("user/filter")]
        [HttpGet]
        public IActionResult Filter()
        {
            try
            {
                var filters = HttpContext.Request.Query;
                return Ok(_user.Filter(filters));
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [Route("user/email-confirmation/{accessKey}")]
        [HttpGet]
        public IActionResult EmailConfirmation(string accessKey)
        {
            try
            {
                return Ok(_user.ConfirmUserEmail(accessKey));
            } catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}
