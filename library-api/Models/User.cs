using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace library_api.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int TypeID { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        public string AccessKey { get; set; }
        public string Picture { get; set; }

        public User Create()
        {
            return this;
        }

        public User Update(int id)
        {
            return this;
        }

        public bool Delete(int userID)
        {
            return true;
        }

        public IList List()
        {
            IList users = new List<User>
            {
                this
            };
            return users;
        }

        public User Get(int id)
        {
            return this;
        }
    }

}
