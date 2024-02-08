using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using Controle_Financeiro___Back.Data;
using Controle_Financeiro___Back.Data.Dtos;
using Controle_Financeiro___Back.Middleware;
using Controle_Financeiro___Back.Models;
using Controle_Financeiro___Back.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Controle_Financeiro___Back.Controllers;

[ApiController]
[Route("[controller]")]
public class ExpenseController : ControllerBase
{
    private FinaceContext _context;
    private IMapper _mapper;
    private UserIdMiddleware _userIdMiddleware;
    private ExpenseService _expenseService;

    public ExpenseController(ExpenseService expenseService, FinaceContext context, IMapper mapper, IHttpContextAccessor httpContext, UserIdMiddleware userIdMiddleware, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _userIdMiddleware = userIdMiddleware;
        _expenseService = expenseService;
    }
    [HttpPost]

    public async Task<IActionResult> AddExpense([FromBody] CreateExpenseRequest expenseDto)
    {
        var expense = await _expenseService.CreateExpenseAsync(expenseDto);
        return CreatedAtAction(nameof(GetExpenseById),
        new { id = expense.Id },
        expense);
    }

    [HttpGet]
    public async Task<IActionResult> GetExpense([FromQuery] int take = 5, [FromQuery] int skip = 0)
    {
        var result = await _expenseService.GetExpensesAsync(take, skip);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetExpenseById(int id)
    {
        var userId = _userIdMiddleware.GetUserId();
        if (userId == null) return Unauthorized();
        var expense = await _context.Expenses
        .FirstOrDefaultAsync(expense =>
        expense.Id == id && expense.UserId == userId);
        if (expense == null) return NotFound();
        var result = _mapper.Map<ReadExpenseResponse>(expense);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExpense(int id, [FromBody] UpdateExpenseRequest expenseDto)
    {
        var userId = _userIdMiddleware.GetUserId();
        if (userId == null) return Unauthorized();
        var expense = await _context.Expenses
        .FirstOrDefaultAsync(expense =>
        expense.Id == id && expense.UserId == userId);
        if (expense == null) return NotFound();
        var type = await _context.Type.FirstOrDefaultAsync(type => type.Name == expenseDto.Name);
        if (type == null) return NotFound();
        expense.TypeId = type.Id;
        expenseDto.Date = expenseDto.Date.ToUniversalTime();
        _mapper.Map(expenseDto, expense);
        _context.SaveChanges();
        return Ok(expense);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExpense(int id)
    {
        var userId = _userIdMiddleware.GetUserId();
        if (userId == null) return Unauthorized();
        var expense = await _context.Expenses.FirstOrDefaultAsync(expense => expense.Id == id && expense.UserId == userId);
        if (expense == null) return NotFound();

        _context.Expenses.Remove(expense);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
