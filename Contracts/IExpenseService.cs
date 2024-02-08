using Controle_Financeiro___Back.Data.Dtos;
using Controle_Financeiro___Back.Models;
using Controle_Financeiro___Back.UseCases.ExpenseUseCase.Response;

namespace Controle_Financeiro___Back.Contracts;
public interface IExpenseService
{
    Task<CreateExpenseResponse> CreateExpenseAsync(CreateExpenseRequest expenseDto);
    Task<ICollection<ReadExpenseResponse>> GetExpensesAsync(int take = 5, int skip = 0);
    Task<ReadExpenseResponse> GetExpenseByIdAsync(int id);
    Task<Expense> UpdateExpenseAsync(UpdateExpenseRequest expenseDto);
    Task DeleteExpenseAsync(int id);
}
