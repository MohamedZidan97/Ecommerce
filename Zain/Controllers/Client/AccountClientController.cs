using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zain.Application.Contracts;
using static Zain.Application.Features.Account.AccountDto;

namespace Zain.Controllers.Client
{
    [Route("api/user/")]
    [ApiController]
    public class AccountClientController : ControllerBase
    {
        private readonly IAuthService authService;

        public AccountClientController(IAuthService authService)
        {
            this.authService = authService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] AccountRegisterDto request)
        {
            var resopnse = await authService.RegisterAsync(request,"client");

            if (!resopnse.Success)
                return BadRequest(resopnse);


            return Ok(resopnse);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AccountGetTokenDto request)
        {
            var resopnse = await authService.GetTokenAsync(request);

            if (!resopnse.Success)
                return BadRequest(resopnse);


            return Ok(resopnse);
        }



        //[HttpGet("listRolesInterface")]
        //public async Task<IActionResult> ListRolesInterface()
        //{
        //    var list = await send.ListRolesAsync();

        //    return Ok(list);
        //}

    }
}
