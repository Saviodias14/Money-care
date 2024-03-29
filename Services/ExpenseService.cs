using AutoMapper;
using Controle_Financeiro___Back.Contracts;
using Controle_Financeiro___Back.Data;
using Controle_Financeiro___Back.Data.Dtos;
using Controle_Financeiro___Back.Middleware;
using Controle_Financeiro___Back.Models;
using Microsoft.EntityFrameworkCore;
using Controle_Financeiro___Back.UseCases.ExpenseUseCase.Response;
using Controle_Financeiro___Back.Controllers;

namespace Controle_Financeiro___Back.Services;
public class ExpenseService : IExpenseService
{
    private readonly FinaceContext _context;
    private readonly IMapper _mapper;
    private readonly UserIdMiddleware _userIdMiddleware;
    private readonly TypeController _typeController;

    public ExpenseService(FinaceContext context, IMapper mapper, UserIdMiddleware userIdMiddleware, TypeController typeController)
    {
        _context = context;
        _mapper = mapper;
        _userIdMiddleware = userIdMiddleware;
        _typeController = typeController;
    }

    public async Task<CreateExpenseResponse> CreateExpenseAsync(CreateExpenseRequest expenseDto)
    {
        var userId = await _userIdMiddleware.GetUserId() ?? throw new Exception("Unauthorized");

        var type = await _context.Type
        .FirstOrDefaultAsync(type =>
        type.Name.ToLower() == expenseDto.Type.Name.ToLower()) ??
        await _typeController.CreateType(expenseDto.Type);

        expenseDto.Date = expenseDto.Date.ToUniversalTime();
        var expense = _mapper.Map<Expense>(expenseDto);

        expense.TypeId = type.Id;
        expense.Type = type;
        expense.UserId = userId;
        await _context.Expenses.AddAsync(expense);
        await _context.SaveChangesAsync();

        return _mapper.Map<CreateExpenseResponse>(expense);

    }
    public async Task<ICollection<ReadExpenseResponse>> GetExpensesAsync(int take = 5, int skip = 0)
    {
        var userId = await _userIdMiddleware.GetUserId() ?? throw new Exception("Unauthorized");
        var expenses = await _context.Expenses
            .Where(x => x.UserId.Equals(userId))
            .Skip(skip)
            .Take(take)
            .ToListAsync();
        var result = _mapper.Map<List<ReadExpenseResponse>>(expenses);
        return result;
    }

    public async Task<ReadExpenseResponse> GetExpenseByIdAsync(int id)
    {
        var userId = await _userIdMiddleware.GetUserId() ?? throw new UnauthorizedAccessException("Unauthorized");
        var expense = await FindUserExpenseById(userId, id) ?? throw new Exception("NotFound");
        var result = _mapper.Map<ReadExpenseResponse>(expense);
        return result;
    }

    public async Task<UpdateExpenseResponse> UpdateExpenseAsync(UpdateExpenseRequest expenseDto, int id)
    {
        var userId = await _userIdMiddleware.GetUserId() ?? throw new Exception("Unauthorized");
        var expense = await FindUserExpenseById(userId, id) ?? throw new Exception("NotFound");
        var type = await _context.Type.FirstOrDefaultAsync(type => type.Name == expenseDto.Type.Name) ??
        await _typeController.CreateType(expenseDto.Type);
        expense.TypeId = type.Id;
        expense.Date = expenseDto.Date.ToUniversalTime();
        var result = _mapper.Map(expenseDto, expense);
        _context.SaveChanges();
        return _mapper.Map<UpdateExpenseResponse>(result);
    }

    public async Task DeleteExpenseAsync(int id)
    {
        var userId = await _userIdMiddleware.GetUserId() ?? throw new Exception("Unauthorized");
        var expense = await FindUserExpenseById(userId, id) ?? throw new Exception("NotFound");

        _context.Expenses.Remove(expense);
        await _context.SaveChangesAsync();
    }

    public async Task<Expense?> FindUserExpenseById(string userId, int id)
        => await _context.Expenses.FirstOrDefaultAsync(expense => expense.Id == id && expense.UserId == userId);
}
