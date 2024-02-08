
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace Controle_Financeiro___Back.Middleware;
public class UserIdMiddleware
{
    private readonly IHttpContextAccessor _httpContext;

    public UserIdMiddleware(IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext;
    }
    [Authorize]
    public string? GetUserId()
    {
        try
        {
            var token = _httpContext.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            var userId = jsonToken.Claims.First(claim => claim.Type == "id").Value;
            return userId;
        }
        catch
        {
            return null;
        }
    }
}
