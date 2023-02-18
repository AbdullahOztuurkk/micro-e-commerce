using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BasketService.Application.Services
{
    public interface IIdentityService
    {
        string GetUserName();
    }

    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public IdentityService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string GetUserName()
        {
            return httpContextAccessor.HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;
        }
    }
}
