using System.Linq;

namespace library_api.Models
{
    public class Login
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public User CheckLogin(Login login)
        {
            // Valida se existe um usuário com o email e senha passadas
            using MyDbContext db = new MyDbContext();
            User user = db.Users.Where(u => u.Email == login.Email).FirstOrDefault();
            if (user != null)
            {
                return BCrypt.Net.BCrypt.Verify(login.Password, user.Password) ? user : null;
            } 

            return null;
        }
    }
}
