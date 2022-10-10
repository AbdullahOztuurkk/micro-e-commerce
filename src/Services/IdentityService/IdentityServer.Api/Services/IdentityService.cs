using IdentityServer.Api.Data;
using IdentityServer.Api.Models;
using IdentityServer.Api.Models.Request;
using IdentityServer.Api.Models.Response;
using IdentityServer.Api.Services.Builder;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace IdentityServer.Api.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly AuthContext context;
        public IdentityService(AuthContext context)
        {
            this.context = context;
        }
        public Task<ResponseModel> Login(LoginRequestModel loginRequest)
        {
            var user = context.Users.AsNoTracking().SingleOrDefault(p => p.Email == loginRequest.Email && p.Password == loginRequest.Password);

            if (user != null)
            {
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name,user.FullName),
                };

                var encodedJwt = TokenBuilder.GetToken(claims);

                return Task.FromResult(new ResponseModel(user.FullName, encodedJwt));
            }

            return null;
        }

        public async Task<ResponseModel> Register(RegistrationRequestModel registrationRequest)
        {
            var user = await context.Users.AddAsync(
                new User { 
                    Email = registrationRequest.Email,
                    Password = registrationRequest.Password,
                    FullName = registrationRequest.FullName });

            await context.SaveChangesAsync();

            var claims = new Claim[]
            {
                    new Claim(ClaimTypes.Email, registrationRequest.Email),
                    new Claim(ClaimTypes.Name,registrationRequest.FullName),
            };

            var encodedJwt = TokenBuilder.GetToken(claims);

            return new ResponseModel(registrationRequest.FullName, encodedJwt);
        }
    }
}
