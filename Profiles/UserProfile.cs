using AutoMapper;
using Controle_Financeiro___Back.Data.Dtos;
using Controle_Financeiro___Back.Models;

namespace Controle_Financeiro___Back.Profiles;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserDto, Users>();
        CreateMap<Users, ReadUserDto>();
    }
}