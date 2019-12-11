using System;
using library_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace library_api.Controllers
{
    internal class AuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string AccessKey = context.HttpContext.Request.Headers["Authorization"];

            if (AccessKey == null)
            {
                context.Result = new UnauthorizedResult();
            } else
            {
                User user = new User();
                if (user.CheckAccessKey(AccessKey) == null)
                {
                    context.Result = new UnauthorizedResult();
                }
            }
        }
    }
}