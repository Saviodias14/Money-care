
using Controle_Financeiro___Back.Data.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Controle_Financeiro___Back.Controllers;
[ApiController]
[Route("[Controller]")]
public class SingUpController : ControllerBase
{
    [HttpPost]
    public IActionResult SingUp (CreateUserDto userDto)
    {
        throw new NotImplementedException();
    }
}
