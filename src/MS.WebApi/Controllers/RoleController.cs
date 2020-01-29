using Microsoft.AspNetCore.Mvc;
using MS.Models.ViewModel;
using MS.WebCore.Core;
using System.Threading.Tasks;

namespace MS.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        [HttpPost]
        public async Task<ExecuteResult> Post(RoleViewModel viewModel)
        {
            return new ExecuteResult();
        }
    }
}