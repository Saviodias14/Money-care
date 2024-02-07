using AutoMapper;
using Controle_Financeiro___Back.Data.Dtos;
using Controle_Financeiro___Back.Models;

namespace Controle_Financeiro___Back.Properties;
public class ExpensesProfile : Profile
{
    public ExpensesProfile()
    {
        CreateMap<CreateExpenseDto, Expense>();
        CreateMap<UpdateExpenseDto, Expense>();
        CreateMap<Expense, ReadExpenseDto>()
                .ForMember(expenseDto => expenseDto.User,
                    opt => opt.MapFrom(expense => expense.User))
                .ForMember(expenseDto => expenseDto.Type,
                    opt => opt.MapFrom(expense => expense.Type));
    }
}
