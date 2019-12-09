using System;
using System.ComponentModel.DataAnnotations;

namespace library_api.Models
{
    public class UserType
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
