using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SharedLibrary.Authentication;
using System.Text;

namespace WebAPI.OptionsSetup;

public class JwtBearerOptionsSetup(IOptions<JwtOptions> jwtOptions) : IConfigureOptions<JwtBearerOptions>
{
    public void Configure(JwtBearerOptions options)
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtOptions.Value.Issuer,
            ValidAudience = jwtOptions.Value.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey)),
            ClockSkew = TimeSpan.Zero
        };
    }
}
