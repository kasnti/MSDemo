using Microsoft.AspNetCore.Mvc;
using MS.Models.ViewModel;
using MS.Services;
using MS.WebCore.Core;
using System.Threading.Tasks;

namespace MS.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoleController : AuthorizeController
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        public async Task<ExecuteResult> Post(RoleViewModel viewModel)
        {
            return await _roleService.Create(viewModel);
        }

        [HttpPut]
        public async Task<ExecuteResult> Put(RoleViewModel viewModel)
        {
            return await _roleService.Update(viewModel);
        }

        [HttpDelete]
        public async Task<ExecuteResult> Delete(long id)
        {
            return await _roleService.Delete(new RoleViewModel { Id = id });
        }
    }
}