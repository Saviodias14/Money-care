using AutoMapper;
using Controle_Financeiro___Back.Contracts;
using Controle_Financeiro___Back.Data;
using Controle_Financeiro___Back.Data.Dtos;
using Controle_Financeiro___Back.Middleware;
using Controle_Financeiro___Back.Models;
using Microsoft.EntityFrameworkCore;
using Controle_Financeiro___Back.UseCases.ExpenseUseCase.Response;

namespace Controle_Financeiro___Back.Services;
public class ExpenseService : IExpenseService
{
    private FinaceContext _context;
    private IMapper _mapper;
    private UserIdMiddleware _userIdMiddleware;

    public ExpenseService(FinaceContext context, IMapper mapper, UserIdMiddleware userIdMiddleware)
    {
        _context = context;
        _mapper = mapper;
        _userIdMiddleware = userIdMiddleware;
    }

    public async Task<CreateExpenseResponse> CreateExpenseAsync(CreateExpenseRequest expenseDto)
    {
        var userId = _userIdMiddleware.GetUserId() ?? throw new UnauthorizedAccessException();
        var type = await _context.Type
        .FirstOrDefaultAsync(type =>
        type.Name == expenseDto.Type.Name) ?? throw new Exception("Esse tipo não existe");
        expenseDto.Date = expenseDto.Date.ToUniversalTime();
        var expense = _mapper.Map<Expense>(expenseDto);
        expense.TypeId = type.Id;
        expense.Type = type;
        expense.UserId = userId;
        await _context.Expenses.AddAsync(expense);
        await _context.SaveChangesAsync();

        return _mapper.Map<CreateExpenseResponse>(expense);

    }

    public Task DeleteExpenseAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ReadExpenseResponse> GetExpenseByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<ReadExpenseResponse>> GetExpensesAsync(int take = 5, int skip = 0)
    {
        var userId = _userIdMiddleware.GetUserId() ?? throw new UnauthorizedAccessException();
        var expenses = await _context.Expenses
            .Where(x => x.UserId.Equals(userId))
            .Skip(skip)
            .Take(take)
            .ToListAsync();
        var result = _mapper.Map<List<ReadExpenseResponse>>(expenses);
        return result;
    }

    public Task<Expense> UpdateExpenseAsync(UpdateExpenseRequest expenseDto)
    {
        throw new NotImplementedException();
    }
}
