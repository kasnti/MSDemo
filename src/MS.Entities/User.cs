using MS.Entities.Core;

namespace MS.Entities
{
    public class User : BaseEntity
    {
        public string Account { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public long RoleId { get; set; } 

        public Role Role { get; set; }
    }
}
