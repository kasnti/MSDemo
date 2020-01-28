using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Entities;
using MS.Entities.Core;

namespace MS.DbContexts
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("TblUsers");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedNever();
            builder.HasIndex(c => c.Account).IsUnique();//指定索引
            builder.Property(c => c.Account).IsRequired().HasMaxLength(16);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Email).HasMaxLength(100);
            builder.Property(c => c.Phone).HasMaxLength(25);
            builder.Property(c => c.RoleId).IsRequired();
            builder.Property(c => c.StatusCode).IsRequired().HasDefaultValue(StatusCode.Enable);
            builder.Property(c => c.Creator).IsRequired();
            builder.Property(c => c.CreateTime).IsRequired();
            builder.Property(c => c.Modifier);
            builder.Property(c => c.ModifyTime);

            builder.HasOne(c => c.Role);
            //builder.HasQueryFilter(b => b.StatusCode != StatusCode.Deleted);//默认不查询软删除数据
        }
    }
} 