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

        public object Create()
        {
            try
            {
                GenerateAccessKey();
                BcryptPassword();
                db.Users.Add(this);
                db.SaveChanges();
                return db.Users.Find(Id);
            } catch (ArgumentException e)
            {
                return e.Message;
            }
        }

        public object Update()
        {
            try
            {
                BcryptPassword();
                db.Users.Update(this);
                db.SaveChanges();
                return db.Users.Find(Id);
            } catch (ArgumentException e)
            {
                return e.Message;
            }
        }

        public object Delete()
        {
            try
            {
                db.Users.Remove(this);
                db.SaveChanges();
                return true;
            } catch(ArgumentException e)
            {
                return e.Message;
            }
        }

        public object List()
        {
            try
            {
                return db.Users.ToList();
            } catch (ArgumentException e)
            {
                return e.Message;
            }
        }

        public object Get(int id)
        {
            try
            {
                return db.Users.Find(id);
            } catch (ArgumentException e)
            {
                return e.Message;
            }
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

        public object CheckAccessKey(string key)
        {
            try
            {
                return db.Users.Where(u => u.AccessKey == key).FirstOrDefault();
            }
            catch (ArgumentException e)
            {
                return e.Message;
            }
        }

        public object Filter(IQueryCollection filters)
        {
            try
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
            catch (ArgumentException e)
            {
                return e.Message;
            }
        }
    }

}
