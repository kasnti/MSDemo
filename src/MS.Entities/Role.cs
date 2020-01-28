using MS.Entities.Core;

namespace MS.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Remark { get; set; } 
    }
}
