using Microsoft.AspNetCore.Http;

namespace Application.Interfaces;
public interface IJwtTokenGenerator
{
    string GenerateToken(Guid userId);
    string GenerateRefreshToken();
    Guid? ValidateToken(string token);
}