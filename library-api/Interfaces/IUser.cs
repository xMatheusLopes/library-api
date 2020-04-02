using System.Collections.Generic;
using library_api.Entities;
using Microsoft.AspNetCore.Http;

namespace library_api.Interfaces {
    public interface IUser
    {   
        User Create();
        User Update();
        int Delete();
        List<User> List();
        User Get(int id);
        void BcryptPassword();
        void GenerateAccessKey();
        User CheckAccessKey(string key);
        List<User> Filter(IQueryCollection filter);
        object SendEmailConfirmation(User user, string path, string baseurl);
        bool ConfirmUserEmail(string accessKey);
    }
}