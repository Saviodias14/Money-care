using Controle_Financeiro___Back.Data.Dtos;
using Controle_Financeiro___Back.Data.Dtos.Users;
using Controle_Financeiro___Back.Services;
using Microsoft.AspNetCore.Mvc;

namespace Controle_Financeiro___Back.Controllers;
[ApiController]
[Route("[Controller]")]
public class UserController : ControllerBase
{
    private UserService _UserService;

    public UserController(UserService UserService)
    {
        _UserService = UserService;
    }
    [HttpPost("SingUp")]
    public async Task<IActionResult> SingUp(CreateUserDto userDto)
    {
        await _UserService.SingUpUser(userDto);
        return Ok("Usuário Cadastrado");
    }
    [HttpPost("SingIn")]
    public async Task<IActionResult> Login(LoginUserDto userDto)
    {
        var token = await _UserService.LoginAsync(userDto);
        return Ok(token);
    }
}
