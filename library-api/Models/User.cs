using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace library_api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int TypeID { get; set; }
        public string CPF { get; set; }
        public string AccessKey { get; set; }
        public string Picture { get; set; }
        public int GeneralStatusID { get; set; }
        private MyDbContext db;

        public User()
        {
            db = new MyDbContext();
        }

        public User Create()
        {
            // Gera o token de acesso
            GenerateAccessKey();
            // Gera uma senha criptografada
            BcryptPassword();

            // Add novo usuário
            db.Users.Add(this);
            db.SaveChanges();
            return db.Users.Find(Id);
        }

        public User Update()
        {
            // Criptografa a senha
            BcryptPassword();

            // Atualiza o usuário
            db.Users.Update(this);
            db.SaveChanges();
            return db.Users.Find(Id);
        }

        public int Delete()
        {
            // Deleta um usuário
            db.Users.Remove(this);
            return db.SaveChanges();
        }

        public List<User> List()
        {
            // Lista os usuários
            return db.Users.ToList();
        }

        public User Get(int id)
        {
            // Pega um usuário
            return db.Users.Find(id);
        }

        public void BcryptPassword()
        {
            // Criptografa senha
            Password = BCrypt.Net.BCrypt.HashPassword(Password);
        }

        public void GenerateAccessKey()
        {
            // Gera um token de acesso
            Guid g = Guid.NewGuid();
            AccessKey = Convert.ToBase64String(g.ToByteArray());
        }

        public User CheckAccessKey(string key)
        {
            // Valida se a chave de acesso é válida
            return db.Users.Where(u => u.AccessKey == key).FirstOrDefault();
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
            return db.Users.FromSqlRaw(
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
                db.Users.Update(user);
                return db.SaveChanges() != 0;
            }

            return false;
        }
    }

}
