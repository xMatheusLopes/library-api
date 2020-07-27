using System;
using System.Linq;
using library_api.Entities;
using library_api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace library_api.Services
{
    public class Login : ILogin
    {
        private readonly MyDbContext db;

        public Login(MyDbContext db)
        {
            this.db = db;
        }

        public string Email { get; set; }
        public string Password { get; set; }

        public User CheckLogin(Login login)
        {
            // Valida se existe um usuário com o email e senha passadas
            User user = db.Users.Where(u => u.Email == login.Email).FirstOrDefault();
            if (user != null)
            {
                return BCrypt.Net.BCrypt.Verify(login.Password, user.Password) ? user : null;
            } 

            return null;
        }

        public User RenewSession(string accessKey) {
            User user = db.Users.Where(u => u.AccessKey == accessKey).FirstOrDefault();

            if (user != null) {
                var token = TokenService.GenerateToken(user);
                user.AccessKey = token;
                db.Users.Update(user);
                db.SaveChanges();
                db.Entry(user).Reload();

                return user;
            }

            return null;
        }
    }
}
