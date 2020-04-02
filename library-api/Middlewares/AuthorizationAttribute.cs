using System;
using library_api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace library_api.Controllers
{
    internal class AuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        private IUser _user;
        public AuthorizationAttribute(IUser _user)
        {
            this._user = _user;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string AccessKey = context.HttpContext.Request.Headers["Authorization"];

            if (AccessKey == null)
            {
                context.Result = new UnauthorizedResult();
            }
            else
            {

                if (this._user.CheckAccessKey(AccessKey) == null)
                {
                    context.Result = new UnauthorizedResult();
                }
            }
        }
        
    }
}