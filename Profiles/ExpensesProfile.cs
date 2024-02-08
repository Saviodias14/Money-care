using AutoMapper;
using Controle_Financeiro___Back.Data.Dtos;
using Controle_Financeiro___Back.Data.Dtos.Services;
using Controle_Financeiro___Back.Models;
using Controle_Financeiro___Back.UseCases.ExpenseUseCase.Response;

namespace Controle_Financeiro___Back.Properties;
public class ExpensesProfile : Profile
{
    public ExpensesProfile()
    {
        CreateMap<CreateExpenseRequest, Expense>();
        CreateMap<UpdateExpenseRequest, Expense>();
        CreateMap<Expense, ReadExpenseResponse>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type));
        CreateMap<Expense, CreateExpenseResponse>();
        CreateMap<Expense, UpdateExpenseResponse>();
    }
}
