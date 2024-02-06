using Controle_Financeiro___Back.Data.Dtos;
using Controle_Financeiro___Back.Services;
using Microsoft.AspNetCore.Mvc;

namespace Controle_Financeiro___Back.Controllers;
[ApiController]
[Route("[Controller]")]
public class SingUpController : ControllerBase
{
    private SingUpService _singUpService;

    public SingUpController(SingUpService singUpService)
    {
        _singUpService = singUpService;
    }
    [HttpPost]
    public async Task<IActionResult> SingUp(CreateUserDto userDto)
    {
        await _singUpService.SingUpUser(userDto);
        return Ok("Usuário Cadastrado");
    }
}
