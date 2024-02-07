using AutoMapper;
using Controle_Financeiro___Back.Data.Dtos;
using Controle_Financeiro___Back.Data.Dtos.Users;
using Controle_Financeiro___Back.Models;
using Microsoft.AspNetCore.Identity;

namespace Controle_Financeiro___Back.Services;

public class UserService
{
    private IMapper _mapper;
    private UserManager<Users> _userManager;
    private SignInManager<Users> _singInManager;
    private TokenService _tokenService;

    public UserService(IMapper mapper, UserManager<Users> userManager, SignInManager<Users> signInManager, TokenService tokenService)
    {
        _mapper = mapper;
        _userManager = userManager;
        _singInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task SingUpUser(CreateUserDto userDto)
    {
        Users users = _mapper.Map<Users>(userDto);
        IdentityResult result = await _userManager.CreateAsync(users, userDto.Password);
        if (!result.Succeeded) throw new ApplicationException("Falha ao cadastrar usuário!");
    }

    public async Task<string> LoginAsync(LoginUserDto userDto)
    {
        var (username, password) = userDto;
        var result = await _singInManager.PasswordSignInAsync(username, password, false, false);
        if (!result.Succeeded) throw new ApplicationException("Senha e/ou usuário incorretos");
        var user = _singInManager
        .UserManager
        .Users
        .FirstOrDefault(user =>
        user.NormalizedUserName == userDto.Username.ToUpper());
        var token = _tokenService.GenerateToken(user);
        return token;
    }
}
