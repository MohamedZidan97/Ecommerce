using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Zain.Controllers.User
{
    [Route("api/user/[controller]")]
    [ApiController]
    public class AccountUserController : ControllerBase
    {
        [HttpGet]
        public IActionResult get()
        {
            return Ok("Ok");
        }
    }
}
