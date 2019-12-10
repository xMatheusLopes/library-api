using System;
using System.Collections;
using System.Collections.Generic;
using library_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace library_api.Controllers
{
    public class UserController
    {
        [Route("users")]
        [HttpGet]
        public object List()
        {
            User user = new User();
            return user.List();
        }

        [Route("user/{userID}")]
        [HttpGet]
        public object Get(int userID)
        {
            User user = new User();
            return user.Get(userID);
        }

        [Route("user")]
        [HttpPost]
        public object Create([FromBody] User user)
        {
            user.Create();
            return user;
        }

        [Route("user/{userID}")]
        [HttpPut]
        public User Update(int userID, [FromBody] User user)
        {
            user.Id = userID;
            user.Update();
            return user;
        }

        [Route("user/{userID}")]
        [HttpDelete]
        public object Delete(int userID)
        {
            User user = new User
            {
                Id = userID
            };
            return user.Delete();
        }
    }
}
