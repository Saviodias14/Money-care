
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Controle_Financeiro___Back.Models;
using Microsoft.IdentityModel.Tokens;

namespace Controle_Financeiro___Back.Services;
public class TokenService
{
    public string GenerateToken(Users users)
    {
        Claim[] claims = new Claim[]
        {
            new("username", users.UserName),
            new("id", users.Id),
            new("email", users.Email)
        };
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("ERSDTFYGUHIJN23W4E5RT6Y7U8I930REHUSDDSCCDLEWUIEFU"));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            expires: DateTime.Now.AddDays(2),
            claims: claims,
            signingCredentials: signingCredentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
