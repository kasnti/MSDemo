using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Entities;

namespace MS.DbContexts
{
    public class UserLoginMap : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.ToTable("TblUserLogins");
            builder.HasKey(c => c.Account);
            //builder.Property(c => c.UserId).ValueGeneratedNever();
            builder.Property(c => c.Account).IsRequired().HasMaxLength(20);
            builder.Property(c => c.HashedPassword).IsRequired().HasMaxLength(256);
            builder.Property(c => c.LastLoginTime);
            builder.Property(c => c.AccessFailedCount).IsRequired().HasDefaultValue(0);
            builder.Property(c => c.IsLocked).IsRequired();
            builder.Property(c => c.LockedTime);
            builder.HasOne(c => c.User);
        }
    }
} 