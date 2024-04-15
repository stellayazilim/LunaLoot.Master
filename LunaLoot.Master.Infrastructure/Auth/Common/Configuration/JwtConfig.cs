using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace LunaLoot.Master.Infrastructure.Auth;

public static class JwtConfig
{


    public static  Func<IServiceCollection, IConfigurationManager, IServiceCollection> _useJwt= (service, config) => UseJWT(service, config);
    public static IServiceCollection UseJWT(this IServiceCollection services, IConfiguration configurationManager)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            SecurityKey securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configurationManager.GetSection("Jwt:SigningKey").Value!));
            SigningCredentials signingCredentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha512Signature);
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                RequireExpirationTime = true,
                RequireSignedTokens = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = configurationManager.GetSection("Jwt:Issuer").Value,
                ValidAudience = configurationManager.GetSection("Jwt:Audience").Value,
                TokenDecryptionKey = securityKey
               
            };
        });
        return services;
    }
}