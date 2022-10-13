using IdentityServer.Api.Models.Request;
using IdentityServer.Api.Models.Response;
using IdentityServer.Api.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IdentityServer.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IIdentityService IdentityService;
        public AuthController(IIdentityService ıdentityService)
        {
            IdentityService = ıdentityService;
        }

        // POST api/auth/login
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(ResponseModel),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel request)
        {
            var result = await IdentityService.Login(request);
            return  result is null ? BadRequest() : Ok(result);
        }

        // POST api/auth/register
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestModel request)
        {
            var result = await IdentityService.Register(request);
            return result is null ? BadRequest() : Ok(result);
        }
    }
}
