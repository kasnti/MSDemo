using MS.Common.Security;
using MS.DbContexts;
using MS.Entities;
using MS.Entities.Core;
using MS.UnitOfWork;
using System;

namespace MS.WebApi
{
    public static class DBSeed
    {
        /// <summary>
        /// 数据初始化
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <returns>返回是否创建了数据库（非迁移）</returns>
        public static bool Initialize(IUnitOfWork<MSDbContext> unitOfWork)
        {
            bool isCreateDb = false;
            //直接自动执行迁移,如果它创建了数据库，则返回true
            if (unitOfWork.DbContext.Database.EnsureCreated())
            {
                isCreateDb = true;
                //打印log-创建数据库及初始化期初数据

                long rootUserId = 1219490056771866624;

                #region 角色、用户、登录
                Role rootRole = new Role
                {
                    Id = 1219490056771866625,
                    Name = "SuperAdmin",
                    DisplayName = "超级管理员",
                    Remark = "系统内置超级管理员",
                    Creator = rootUserId,
                    CreateTime = DateTime.Now
                };
                User rootUser = new User
                {
                    Id = rootUserId,
                    Account = "admin",
                    Name = "admin",
                    RoleId = rootRole.Id,
                    StatusCode = StatusCode.Enable,
                    Creator = rootUserId,
                    CreateTime = DateTime.Now,
                };

                unitOfWork.GetRepository<Role>().Insert(rootRole);
                unitOfWork.GetRepository<User>().Insert(rootUser);
                unitOfWork.GetRepository<UserLogin>().Insert(new UserLogin
                {
                    UserId = rootUserId,
                    Account = rootUser.Account,
                    HashedPassword = Crypto.HashPassword(rootUser.Account),//默认密码同账号名
                    IsLocked = false
                });
                unitOfWork.SaveChanges();

                #endregion
            }
            return isCreateDb;
        }


    }
}
