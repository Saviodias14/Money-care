
using System.IdentityModel.Tokens.Jwt;
using Castle.Core.Internal;
using Controle_Financeiro___Back.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Controle_Financeiro___Back.Middleware;
public class UserIdMiddleware
{
    private readonly IHttpContextAccessor _httpContext;
    private readonly FinaceContext _context;

    public UserIdMiddleware(IHttpContextAccessor httpContext, FinaceContext context)
    {
        _httpContext = httpContext;
        _context = context;
    }
    [Authorize]
    public async Task<string?> GetUserId()
    {
        var HttpContext = _httpContext.HttpContext;
        var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        if (token.IsNullOrEmpty()) throw new Exception("Unauthorized");
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token) as JwtSecurityToken; ;
        var userId = jsonToken.Claims.First(claim => claim.Type == "id").Value;
        var result = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        return result?.Id;
    }
}