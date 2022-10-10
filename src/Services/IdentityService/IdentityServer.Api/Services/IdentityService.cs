using IdentityServer.Api.Data;
using IdentityServer.Api.Models;
using IdentityServer.Api.Models.Request;
using IdentityServer.Api.Models.Response;
using IdentityServer.Api.Services.Builder;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Api.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly AuthContext context;
        public IdentityService(AuthContext context)
        {
            this.context = context;
        }
        public async Task<ResponseModel> Login(LoginRequestModel loginRequest)
        {
            var user = await context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Email == loginRequest.Email && p.Password == loginRequest.Password);

            if (user is not null)
            {
                var encodedJwt = user.CreateJsonWebToken();
                return new ResponseModel(user.FullName, encodedJwt);
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

            var encodedJwt = user.Entity.CreateJsonWebToken();

            return new ResponseModel(registrationRequest.FullName, encodedJwt);
        }
    }
}
