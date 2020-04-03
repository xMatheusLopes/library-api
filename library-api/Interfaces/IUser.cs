using System.Collections.Generic;
using library_api.Entities;
using Microsoft.AspNetCore.Http;

namespace library_api.Interfaces {
    public interface IUser
    {   
        User Create(User user);
        User Update(User user);
        int Delete(User user);
        List<User> List();
        User Get(int id);
        string BcryptPassword(string password);
        string GenerateAccessKey();
        User CheckAccessKey(string key);
        List<User> Filter(IQueryCollection filter);
        object SendEmailConfirmation(User user, string path, string baseurl);
        bool ConfirmUserEmail(string accessKey);
    }
}