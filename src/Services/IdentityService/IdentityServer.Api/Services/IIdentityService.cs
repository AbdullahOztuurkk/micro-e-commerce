using IdentityServer.Api.Models.Request;
using IdentityServer.Api.Models.Response;

namespace IdentityServer.Api.Services
{
    public interface IIdentityService
    {
        public Task<ResponseModel> Login(LoginRequestModel loginRequest);
        public Task<ResponseModel> Register(RegistrationRequestModel registrationRequest);
    }
}
