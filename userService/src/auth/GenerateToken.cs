using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public static class JwtHelper
{
    public static string GenerateToken(User user, IConfiguration config)
    {

        var key = config["JwtSettings:Key"];
        var issuer = config["JwtSettings:Issuer"];
        var audience = config["JwtSettings:Audience"];

        var claims = new[]
        {
            new Claim(ClaimTypes.Email, user.UserEmail),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())

        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


        var tokenDescriptor = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: signingCredentials
        );


        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}