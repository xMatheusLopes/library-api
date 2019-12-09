using System;
using System.Collections;
using library_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace library_api.Controllers
{
    public class UserController
    {
        [Route("user")]
        [HttpPost]
        public IList List()
        {
            User user = new User();
            return user.List();
        }

        [Route("user/{:userID}")]
        [HttpPost]
        public User Get(int userID)
        {
            User user = new User();
            return user.Get(userID);
        }

        [Route("user/create")]
        [HttpPost]
        public User Create([FromBody] User user)
        {
            user.Create();
            return user;
        }

        [Route("user/{:userID}/create")]
        [HttpPost]
        public User Update(int userID, [FromBody] User user)
        {
            user.Update(userID);
            return user;
        }

        [Route("user/{:userID}/delete")]
        [HttpPost]
        public bool Delete(int userID)
        {
            User user = new User();
            return user.Delete(userID);
        }
    }
}
