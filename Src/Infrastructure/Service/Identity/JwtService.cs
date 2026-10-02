using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace SwaOlova.Infrastructure.Service.Identity;

public sealed class JwtService(IConfiguration configuration) : IJwtService
{
    public string GenerateToken(Guid userId, string email, string role)
    {
        var key = GetSigningKey();
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var tokenHandler = new JwtSecurityTokenHandler();
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(ClaimTypes.Role, role),
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(GetExpirationMinutes()),
            signingCredentials: credentials);

        return tokenHandler.WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var bytes = new byte[64];
        RandomNumberGenerator.Fill(bytes);
        return Convert.ToBase64String(bytes);
    }

    private SymmetricSecurityKey GetSigningKey()
    {
        var secret = configuration["Jwt:Key"] ?? configuration["Jwt:Secret"] ?? "development-only-secret-key-change-me";
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
    }

    private double GetExpirationMinutes()
        => double.TryParse(configuration["Jwt:ExpiresInMinutes"], out var value) && value > 0 ? value : 60d;
}