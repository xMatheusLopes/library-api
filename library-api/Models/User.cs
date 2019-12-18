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
        private MyDbContext db;

        public User()
        {
            db = new MyDbContext();
        }

        public User Create()
        {
            GenerateAccessKey();
            BcryptPassword();
            db.Users.Add(this);
            db.SaveChanges();
            return db.Users.Find(Id);
        }

        public User Update()
        {
            BcryptPassword();
            db.Users.Update(this);
            db.SaveChanges();
            return db.Users.Find(Id);
        }

        public int Delete()
        {
            db.Users.Remove(this);
            return db.SaveChanges();
        }

        public List<User> List()
        {
            return db.Users.ToList();
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public void BcryptPassword()
        {
            Password = BCrypt.Net.BCrypt.HashPassword(Password);
        }

        public void GenerateAccessKey()
        {
            Guid g = Guid.NewGuid();
            AccessKey = Convert.ToBase64String(g.ToByteArray());
        }

        public User CheckAccessKey(string key)
        {
            return db.Users.Where(u => u.AccessKey == key).FirstOrDefault();
        }

        public List<User> Filter(IQueryCollection filters)
        {
            List<string> whereClause = new List<string>();

            foreach (var filter in filters)
            {
                whereClause.Add($"{ filter.Key } = '{ filter.Value }'");
            }
            return db.Users.FromSqlRaw(
                $"SELECT * FROM Users " +
                $"WHERE { String.Join(" AND ", whereClause) }")
                .ToList();
        }
    }

}
