using AutoMapper;
using Controle_Financeiro___Back.Data.Dtos.Services;
using Controle_Financeiro___Back.Models;
using Controle_Financeiro___Back.UseCases.TypeUseCase.Response;

namespace Controle_Financeiro___Back.Profiles;
public class TypeProfile : Profile
{
    public TypeProfile()
    {
        CreateMap<CreateTypeRequest, Models.Type>();
        CreateMap<Models.Type, ReadTypesResponse>();
        CreateMap<UpdateTypeRequest, Models.Type>();
    }
}
