using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MS.DbContexts
{
    public class MSDbContext : DbContext
    {
        //Add-Migration InitialCreate
        //Update-Database InitialCreate
        public MSDbContext(DbContextOptions<MSDbContext> options)
           : base(options)
        {
        }
        //此处用微软原生的控制台日志记录，如果使用NLog很可能数据库还没创建，造成记录日志到数据库性能下降（一直在尝试连接数据库，但是数据库还没创建）
        //此处使用静态实例，这样不会为每个上下文实例创建新的 ILoggerFactory 实例，这一点非常重要。 否则会导致内存泄漏和性能下降。
        //此处使用了Debug和console两种日志输出，会输出到控制台和调试窗口
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => builder.AddDebug().AddConsole());
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(MyLoggerFactory);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LogrecordMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new UserLoginMap());
            modelBuilder.ApplyConfiguration(new UserMap());

            base.OnModelCreating(modelBuilder);
        }
        
    }
} 