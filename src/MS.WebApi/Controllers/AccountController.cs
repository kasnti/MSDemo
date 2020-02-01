using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Component.Jwt.UserClaim;
using MS.Models.ViewModel;
using MS.Services;
using MS.WebCore.Core;
using System.Threading.Tasks;

namespace MS.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : AuthorizeController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ExecuteResult<UserData>> Login(LoginViewModel viewModel)
        {
            return await _accountService.Login(viewModel);
        }
    }
}