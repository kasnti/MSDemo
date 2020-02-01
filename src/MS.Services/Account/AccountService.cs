using AutoMapper;
using Microsoft.Extensions.Options;
using MS.Common.IDCode;
using MS.Component.Jwt;
using MS.Component.Jwt.UserClaim;
using MS.DbContexts;
using MS.Models.ViewModel;
using MS.UnitOfWork;
using MS.WebCore;
using MS.WebCore.Core;
using System.Threading.Tasks;

namespace MS.Services
{
    public class AccountService : BaseService, IAccountService
    {
        private readonly JwtService _jwtService;
        private readonly SiteSetting _siteSetting;

        public AccountService(JwtService jwtService, IOptions<SiteSetting> options, IUnitOfWork<MSDbContext> unitOfWork, IMapper mapper, IdWorker idWorker, IClaimsAccessor claimsAccessor) : base(unitOfWork, mapper, idWorker, claimsAccessor)
        {
            _jwtService = jwtService;
            _siteSetting = options.Value;
        }

        public async Task<ExecuteResult<UserData>> Login(LoginViewModel viewModel)
        {
            var result = await viewModel.LoginValidate(_unitOfWork, _mapper, _siteSetting);
            if (result.IsSucceed)
            {
                result.Result.Token = _jwtService.BuildToken(_jwtService.BuildClaims(result.Result));
                return new ExecuteResult<UserData>(result.Result);
            }
            else
            {
                return new ExecuteResult<UserData>(result.Message);
            }
        }
    }
}
