using AutoMapper;
using MS.Common.IDCode;
using MS.Component.Jwt.UserClaim;
using MS.DbContexts;
using MS.Entities;
using MS.Models.ViewModel;
using MS.UnitOfWork;
using MS.WebCore.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services
{
    public class RoleService : BaseService, IRoleService
    {
        public RoleService(IUnitOfWork<MSDbContext> unitOfWork, IMapper mapper, IdWorker idWorker, IClaimsAccessor claimsAccessor) : base(unitOfWork, mapper, idWorker, claimsAccessor)
        {
        }

        public async Task<ExecuteResult<Role>> Create(RoleViewModel viewModel)
        {
            ExecuteResult<Role> result = new ExecuteResult<Role>();
            //检查字段
            if (viewModel.CheckField(ExecuteType.Create, _unitOfWork) is ExecuteResult checkResult && !checkResult.IsSucceed)
            {
                return result.SetFailMessage(checkResult.Message);
            }
            using (var tran = _unitOfWork.BeginTransaction())//开启一个事务
            {
                Role newRow = _mapper.Map<Role>(viewModel);
                newRow.Id = _idWorker.NextId();//获取一个雪花Id
                newRow.Creator = _claimsAccessor.UserId;//拿到用户信息
                newRow.CreateTime = DateTime.Now;
                _unitOfWork.GetRepository<Role>().Insert(newRow);
                await _unitOfWork.SaveChangesAsync();
                await tran.CommitAsync();//提交事务

                result.SetData(newRow);//添加成功，把新的实体返回回去
            }
            return result;
        }

        public async Task<ExecuteResult> Delete(RoleViewModel viewModel)
        {
            ExecuteResult result = new ExecuteResult();
            //检查字段
            if (viewModel.CheckField(ExecuteType.Delete, _unitOfWork) is ExecuteResult checkResult && !checkResult.IsSucceed)
            {
                return checkResult;
            }
            _unitOfWork.GetRepository<Role>().Delete(viewModel.Id);
            await _unitOfWork.SaveChangesAsync();//提交
            return result;
        }

        public async Task<ExecuteResult> Update(RoleViewModel viewModel)
        {
            ExecuteResult result = new ExecuteResult();
            //检查字段
            if (viewModel.CheckField(ExecuteType.Update, _unitOfWork) is ExecuteResult checkResult && !checkResult.IsSucceed)
            {
                return checkResult;
            }

            //从数据库中取出该记录
            var row = await _unitOfWork.GetRepository<Role>().FindAsync(viewModel.Id);//在viewModel.CheckField中已经获取了一次用于检查，所以此处不会重复再从数据库取一次，有缓存
            //修改对应的值
            row.Name = viewModel.Name;
            row.DisplayName = viewModel.DisplayName;
            row.Remark = viewModel.Remark;
            row.Modifier = _claimsAccessor.UserId;//拿到用户信息
            row.ModifyTime = DateTime.Now;
            _unitOfWork.GetRepository<Role>().Update(row);
            await _unitOfWork.SaveChangesAsync();//提交

            return result;
        }
    }
}
