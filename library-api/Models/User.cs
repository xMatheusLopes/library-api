using System;
using System.Linq;

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

        public object Create()
        {
            try
            {
                using MyDbContext db = new MyDbContext();
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
                using MyDbContext db = new MyDbContext();
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
                using MyDbContext db = new MyDbContext();
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
                using MyDbContext db = new MyDbContext();
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
                using MyDbContext db = new MyDbContext();
                return db.Users.Find(id);
            } catch (ArgumentException e)
            {
                return e.Message;
            }
        }
    }

}
