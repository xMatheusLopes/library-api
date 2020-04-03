using System;
using library_api.Interfaces;
using library_api.Services;

namespace library_api.Entities
{
    public class Book : BaseApi<Book>, IBook
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Picture { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Book(MyDbContext myDbContext) : base(myDbContext)
        {
        }
    }
}