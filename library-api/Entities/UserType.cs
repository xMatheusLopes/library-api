using library_api.Interfaces;

namespace library_api.Entities
{
    public class UserType : IUserType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
