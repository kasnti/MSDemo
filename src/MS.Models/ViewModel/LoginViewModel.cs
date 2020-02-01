using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MS.Common.Security;
using MS.Component.Jwt.UserClaim;
using MS.DbContexts;
using MS.Entities;
using MS.Entities.Core;
using MS.UnitOfWork;
using MS.WebCore;
using MS.WebCore.Core;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MS.Models.ViewModel
{
    public class LoginViewModel
    {
        [Display(Name = "用户名")]
        [Required(ErrorMessage = "{0}必填")]
        [StringLength(16, ErrorMessage = "不能超过{0}个字符")]
        [RegularExpression(@"^[a-zA-Z0-9_]{4,16}$", ErrorMessage = "只能包含字符、数字和下划线")]
        public string Account { get; set; }
        [Display(Name = "密码")]
        [Required(ErrorMessage = "{0}必填")]
        public string Password { get; set; }

        public async Task<ExecuteResult<UserData>> LoginValidate(IUnitOfWork<MSDbContext> unitOfWork, IMapper mapper, SiteSetting siteSetting)
        {
            ExecuteResult<UserData> result = new ExecuteResult<UserData>();
            //将登录用户查出来
            var loginUserInDB = await unitOfWork.GetRepository<UserLogin>().FindAsync(Account);

            //用户不存在
            if (loginUserInDB is null)
            {
                return result.SetFailMessage("用户不存在");
            }

            //用户被锁定
            if (loginUserInDB.IsLocked &&
                loginUserInDB.LockedTime.HasValue &&
                (DateTime.Now - loginUserInDB.LockedTime.Value).Minutes < siteSetting.LoginLockedTimeout)
            {
                return result.SetFailMessage(string.Format("用户已被锁定，请{0}分钟后再试！", siteSetting.LoginLockedTimeout.ToString()));
            }

            //密码正确
            if (Crypto.VerifyHashedPassword(loginUserInDB.HashedPassword, Password))
            {
                //密码正确后才加载用户信息、角色信息
                var userInDB = await unitOfWork.GetRepository<User>().GetFirstOrDefaultAsync(
                    predicate: a => a.Id == loginUserInDB.UserId,
                    include: source => source
                     .Include(u => u.Role));

                //如果用户已失效
                if (userInDB.StatusCode != StatusCode.Enable)
                {
                    return result.SetFailMessage("用户已失效，请联系管理员！");
                }

                //用户正常、密码正确，更新相应字段
                loginUserInDB.IsLocked = false;
                loginUserInDB.AccessFailedCount = 0;
                loginUserInDB.LastLoginTime = DateTime.Now;
                //提交到数据库
                await unitOfWork.SaveChangesAsync();

                //得到userdata
                UserData userData = mapper.Map<UserData>(userInDB);
                return result.SetData(userData);
            }
            //密码错误
            else
            {
                loginUserInDB.AccessFailedCount++;//失败次数累加
                result.SetFailMessage("用户名或密码错误！");
                //超出失败次数限制
                if (loginUserInDB.AccessFailedCount >= siteSetting.LoginFailedCountLimits)
                {
                    loginUserInDB.IsLocked = true;
                    loginUserInDB.LockedTime = DateTime.Now;
                    result.SetFailMessage(string.Format("用户已被锁定，请{0}分钟后再试！", siteSetting.LoginLockedTimeout.ToString()));
                }
                //提交到数据库
                await unitOfWork.SaveChangesAsync();
                return result;
            }
        }
    }
}