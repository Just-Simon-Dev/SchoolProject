using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Application.Settings;
using SchoolProject.Domain.Entities;

namespace SchoolProject.Application.services;

public interface IJwtService
{
    Task<string> GenerateToken(User user);
}
public class JwtService : IJwtService
{
    private readonly JwtSettings settings;
    public JwtService(IOptions<JwtSettings> jwtSettings)
    {
        settings = jwtSettings.Value;
    }
    public async Task<string> GenerateToken(User user)
    {
        var key = Encoding.UTF8.GetBytes(settings.Key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Login),
                new Claim(JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(60),
            SigningCredentials = new SigningCredentials
            (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var stringToken = tokenHandler.WriteToken(token);
        return stringToken;
    }
}