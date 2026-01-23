using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Surreal.Client.Rest.Test.Unit;

public static class JwtHelper
{
    public static string GenerateToken(string userId = "test-user", int minutesToExpire = 60)
    {
        // 1. Define Claims (The payload)
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("custom-role", "admin") // Add any custom claims your app needs
        };

        // 2. Create a Key (Use a fixed string for reproducibility)
        // Note: The key must be at least 16 characters for HmacSha256
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super-secret-testing-key-1234567"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // 3. Create the Token
        var token = new JwtSecurityToken(
            issuer: "test-issuer",
            audience: "test-audience",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(minutesToExpire),
            signingCredentials: creds
        );

        // 4. Serialize to String
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}