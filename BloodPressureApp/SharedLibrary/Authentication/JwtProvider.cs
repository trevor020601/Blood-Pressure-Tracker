using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using SharedLibrary.Attributes;
using SharedLibrary.BloodPressureDomain.User;
using SharedLibrary.DataAccess;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SharedLibrary.Authentication;

[InjectDependency(ServiceLifetime.Scoped)]
public interface IJwtProvider
{
    Task<string> Generate(User user);
    string GenerateRefreshToken();
}

internal sealed class JwtProvider(IOptions<JwtOptions> options, IApplicationDbContext applicationDbContext) : IJwtProvider
{
    public async Task<string> Generate(User user)
    {
        var secretKey = options.Value.SecretKey;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var policyNames = await applicationDbContext.UserPolicies
            .Where(up => up.UserId == user.Id)
            .Select(up => up.Policy.Name)
            .ToListAsync();

        List<Claim> claims = [
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.Value.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email.Value)
        ];

        claims.AddRange(policyNames.Select(p => new Claim(ClaimTypes.Role, p)));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddMinutes(options.Value.ExpirationInMinutes),
            SigningCredentials = credentials,
            Issuer = options.Value.Issuer,
            Audience = options.Value.Audience
        };

        var handler = new JsonWebTokenHandler();

        return handler.CreateToken(tokenDescriptor);
    }

    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }
}
