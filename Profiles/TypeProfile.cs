using AutoMapper;
using Controle_Financeiro___Back.Data.Dtos.Services;
using Controle_Financeiro___Back.Models;

namespace Controle_Financeiro___Back.Profiles;
public class TypeProfile : Profile
{
    public TypeProfile()
    {
        CreateMap<CreateTypeDto, Models.Type>();
        CreateMap<Models.Type, ReadTypesDto>();
        CreateMap<UpdateTypeDto, Models.Type>();
    }
}
