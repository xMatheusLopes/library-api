using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using library_api.Interfaces;
using library_api.Models;

namespace library_api.Entities
{
    public class User : IUser
    {
        private readonly MyDbContext _db;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual int TypeID { get; set; }
        public UserType Type { get; set; }
        public string CPF { get; set; }
        public string AccessKey { get; set; }
        public string Picture { get; set; }
        public int GeneralStatusID { get; set; }
        public virtual GeneralStatus GeneralStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        public User(MyDbContext myDbContext) {
            _db = myDbContext;
        }

        public User Create(User user)
        {
            // Gera o token de acesso
            user.AccessKey = GenerateAccessKey();
            // Gera uma senha criptografada
            user.Password = BcryptPassword(user.Password);

            // Add novo usuário
            _db.Users.Add(user);
            _db.SaveChanges();
            return user;
        }

        public User Update(User user)
        {
            User oldUser = _db.Users.AsNoTracking().First(u => u.Id == user.Id);
            if (user.Password != null) {
                // Criptografa a senha
                user.Password = BcryptPassword(user.Password);
            } else {
                user.Password = oldUser.Password;
            }

            user.AccessKey = oldUser.AccessKey;

            // Atualiza o usuário
            _db.Users.Update(user);
            _db.SaveChanges();
            return user;
        }

        public int Delete(User user)
        {
            // Deleta um usuário
            _db.Users.Remove(user);
            return _db.SaveChanges();
        }

        public List<User> List()
        {
            // Lista os usuários
            return _db.Users.ToList();
        }

        public User Get(int id)
        {
            // Pega um usuário
            return _db.Users.Find(id);
        }

        public string BcryptPassword(string password)
        {
            // Criptografa senha
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public string GenerateAccessKey()
        {
            // Gera um token de acesso
            Guid g = Guid.NewGuid();
            return Convert.ToBase64String(g.ToByteArray());
        }

        public User CheckAccessKey(string key)
        {
            // Valida se a chave de acesso é válida
            return _db.Users.Where(u => u.AccessKey == key).FirstOrDefault();
        }

        public List<User> Filter(IQueryCollection filters)
        {
            /*
             *   Monta o where de acordo com os parametros (query da URL)
             *   passados ex:
             *   - ?Email=matheus@foo.com&Name=Matheus
             *   monta a clausula:
             *   - WHERE Email = "matheus@foo.com" AND Name = "Matheus"
             */
            List<string> whereClause = new List<string>();

            foreach (var filter in filters)
            {
                whereClause.Add($"{ filter.Key } = '{ filter.Value }'");
            }

            // Retorna a lista de pessoas filtradas
            return _db.Users.FromSqlRaw(
                $"SELECT * FROM Users " +
                $"WHERE { String.Join(" AND ", whereClause) }")
                .ToList();
        }

        public object SendEmailConfirmation(User user, string path, string baseurl)
        {
            EmailCofirmation data = new EmailCofirmation
            {
                Username = user.Name,
                BaseUrl = baseurl,
                ConfirmationUrl = $"{baseurl}user/email-confirmation/{user.AccessKey}"
            };

            Email email = new Email
            {
                From = "matheushl1996@gmail.com",
                To = user.Email
            };
            email.SetBody(data, path);  

            email.SetMessage();
            return email.Send();
        }

        public bool ConfirmUserEmail(string accessKey)
        {
            User user = CheckAccessKey(accessKey);
            if (user != null)
            {
                user.GeneralStatusID = 2;
                _db.Users.Update(user);
                return _db.SaveChanges() != 0;
            }

            return false;
        }
    }

}
