using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MS.WebApi.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class AuthorizeController : ControllerBase
    {
    }
}