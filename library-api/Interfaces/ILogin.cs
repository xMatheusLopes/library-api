using library_api.Entities;
using library_api.Services;

namespace library_api.Interfaces {
    public interface ILogin
    {
        User CheckLogin(Login login);
        User RenewSession(string accessKey);
    }
}