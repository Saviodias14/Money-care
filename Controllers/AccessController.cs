using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controle_Financeiro___Back.Controllers;
[ApiController]
[Route("[Controller]")]
public class AccessController : ControllerBase
{
    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        return Ok("Acesso permitido!");
    }
}
