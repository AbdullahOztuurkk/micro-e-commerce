﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace OrderService.Api.Extensions
{
    public static class AuthRegistration
    {
        public static void ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["AuthConfig:Secret"]));

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingKey
                };
            });
        }
    }
}
