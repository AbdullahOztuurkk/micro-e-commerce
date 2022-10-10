using IdentityServer.Api.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityServer.Api.Services.Builder
{
    public static class TokenBuilder
    {
        public  static string GetToken(this User user)
        {
            var claims = new Claim[]
            {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.FullName),
            };

            var SymmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AuthContextTopSecretLongSymmetricKey"));

            var credentials = new SigningCredentials(SymmetricKey, SecurityAlgorithms.HmacSha256);

            var expiryDate = DateTime.Now.AddDays(10);

            var token = new JwtSecurityToken(claims: claims, expires: expiryDate, signingCredentials: credentials, notBefore: DateTime.Now);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);

            return encodedJwt;
        }
    }
}
