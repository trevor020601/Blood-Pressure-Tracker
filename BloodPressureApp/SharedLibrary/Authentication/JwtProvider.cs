using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using SharedLibrary.Attributes;
using SharedLibrary.BloodPressureDomain.User;
using System.Security.Claims;
using System.Text;

namespace SharedLibrary.Authentication;

[InjectDependency(ServiceLifetime.Singleton)]
public interface IJwtProvider
{
    string Generate(User user);
}

internal sealed class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
{
    public string Generate(User user)
    {
        var secretKey = options.Value.SecretKey;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new(JwtRegisteredClaimNames.Sub, user.Id.Value.ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email.Value)
            ]),
            Expires = DateTime.Now.AddMinutes(options.Value.ExpirationInMinutes),
            SigningCredentials = credentials,
            Issuer = options.Value.Issuer,
            Audience = options.Value.Audience
        };

        var handler = new JsonWebTokenHandler();

        return handler.CreateToken(tokenDescriptor);
    }
}
