using System;

namespace MS.Entities.Core
{
    //没有Id主键的实体继承这个
    public interface IEntity
    {
    }
    //有Id主键的实体继承这个
    public abstract class BaseEntity : IEntity
    {
        public long Id { get; set; }
        public StatusCode StatusCode { get; set; }
        public long? Creator { get; set; }
        public DateTime? CreateTime { get; set; }
        public long? Modifier { get; set; }
        public DateTime? ModifyTime { get; set; }
    }
}
