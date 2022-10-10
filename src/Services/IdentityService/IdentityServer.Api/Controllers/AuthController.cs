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
        public Task<ResponseModel> Login([FromBody] LoginRequestModel request)
        {
            return IdentityService.Login(request);
        }

        // POST api/auth/register
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status200OK)]
        public Task<ResponseModel> Register([FromBody] RegistrationRequestModel request)
        {
            return IdentityService.Register(request);
        }
    }
}
