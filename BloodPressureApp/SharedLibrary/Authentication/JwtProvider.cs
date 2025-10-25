using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SharedLibrary.BloodPressureDomain.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SharedLibrary.Authentication;

internal sealed class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;

    public JwtProvider(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public string Generate(User user)
    {
        var claims = new Claim[] {
            new(JwtRegisteredClaimNames.Sub, user.Id.Value.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email.Value)
        };

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_options.SecretKey)
                ),
                SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            _options.Issuer, 
            _options.Audience, 
            claims, 
            null, 
            DateTime.UtcNow.AddHours(1), 
            signingCredentials
        );

        var tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return tokenValue;
    }
}
