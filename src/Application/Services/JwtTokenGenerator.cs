using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Interfaces;
using Domain.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;

    public JwtTokenGenerator(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public string GenerateToken(Guid userId)
    {
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim("id", userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var securityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiryMinutes),
            claims: claims,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public Guid? ValidateToken(string token)
    {
        if (token == null) return null;

        var tokenValidationParameter = new TokenValidationParameters
        {
            ValidIssuer = _jwtSettings.Issuer,
            ValidateIssuer = true,
            ValidAudience = _jwtSettings.Audience,
            ValidateAudience = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            ValidateIssuerSigningKey = true,
            ValidateLifetime = false,
            // ClockSkew = TimeSpan.Zero,
            // LifetimeValidator = CustomLifetimeValidator,
            // RequireExpirationTime = true,
        };

        var jwtHandler = new JwtSecurityTokenHandler();
        var principal = jwtHandler.ValidateToken(token, tokenValidationParameter, out SecurityToken securityToken);

        var jwtSecurityToken = (JwtSecurityToken)securityToken;
        if (jwtSecurityToken == null)
        {
            return null;
        }

        var id = principal.Claims.Where(x => x.Type == "id").Select(_ => _.Value).FirstOrDefault();

        return Guid.Parse(id!);
    }

}