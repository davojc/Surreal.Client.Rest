using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Surreal.Client.Rest.Test.Unit;

public static class JwtHelper
{
    public static string GenerateToken(string userId = "test-user", int minutesToExpire = 60)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("custom-role", "admin") // Add any custom claims your app needs
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super-secret-testing-key-1234567"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "test-issuer",
            audience: "test-audience",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(minutesToExpire),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}