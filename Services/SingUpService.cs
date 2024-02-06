using AutoMapper;
using Controle_Financeiro___Back.Data.Dtos;
using Controle_Financeiro___Back.Models;
using Microsoft.AspNetCore.Identity;

namespace Controle_Financeiro___Back.Services;

public class SingUpService
{
    private IMapper _mapper;
    private UserManager<Users> _userManager;

    public SingUpService(IMapper mapper, UserManager<Users> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task SingUpUser(CreateUserDto userDto)
    {
        Users users = _mapper.Map<Users>(userDto);
        IdentityResult result = await _userManager.CreateAsync(users, userDto.Password);
        if (!result.Succeeded) throw new ApplicationException("Falha ao cadastrar usuário!");
    }
}
