namespace SwaOlova.Infrastructure.Service.Identity;

public interface IJwtService
{
    string GenerateToken(Guid userId, string email, string role);

    string GenerateRefreshToken();
}