using MS.DbContexts;
using MS.Entities;
using MS.UnitOfWork;
using MS.WebCore.Core;
using System.ComponentModel.DataAnnotations;

namespace MS.Models.ViewModel
{
    public class RoleViewModel
    {
        public long Id { get; set; }
        [Display(Name = "角色名称")]
        [Required(ErrorMessage = "{0}必填")]
        [StringLength(16, ErrorMessage = "不能超过{0}个字符")]
        [RegularExpression(@"^[a-zA-Z0-9_]{4,16}$", ErrorMessage = "只能包含字符、数字和下划线")]
        public string Name { get; set; }
        [Display(Name = "角色显示名")]
        [Required(ErrorMessage = "{0}必填")]
        [StringLength(50, ErrorMessage = "不能超过{0}个字符")]
        public string DisplayName { get; set; }
        [Display(Name = "备注")]
        [StringLength(4000, ErrorMessage = "不能超过{0}个字符")]
        public string Remark { get; set; }

        public ExecuteResult CheckField(ExecuteType executeType, IUnitOfWork<MSDbContext> unitOfWork)
        {
            ExecuteResult result = new ExecuteResult();
            var repo = unitOfWork.GetRepository<Role>();

            //如果不是新增角色，操作之前都要先检查角色是否存在
            if (executeType != ExecuteType.Create && !repo.Exists(a => a.Id == Id))
            {
                return result.SetFailMessage("角色不存在");
            }

            //针对不同的操作，检查逻辑不同
            switch (executeType)
            {
                case ExecuteType.Delete:
                    //删除角色前检查角色下还没有员工
                    if (unitOfWork.GetRepository<User>().Exists(a => a.RoleId == Id))
                    {
                        return result.SetFailMessage("还有员工正在使用该角色，无法删除");
                    }
                    break;
                case ExecuteType.Update:
                    //如果存在Id不同，角色名相同的实体，则返回报错
                    if (repo.Exists(a => a.Name == Name && a.Id != Id))
                    {
                        return result.SetFailMessage($"已存在相同的角色名称：{Name}");
                    }
                    break;
                case ExecuteType.Create:
                default:
                    //如果存在相同的角色名，则返回报错
                    if (repo.Exists(a => a.Name == Name))
                    {
                        return result.SetFailMessage($"已存在相同的角色名称：{Name}");
                    }
                    break;
            }
            return result;//没有错误，默认返回成功
        }
    }
}
