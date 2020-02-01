using MS.Component.Jwt.UserClaim;
using MS.Models.ViewModel;
using MS.WebCore.Core;
using System.Threading.Tasks;

namespace MS.Services
{
    public interface IAccountService : IBaseService
    {
        Task<ExecuteResult<UserData>> Login(LoginViewModel viewModel);
    }
}