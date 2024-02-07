using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Controle_Financeiro___Back.Data;
using Controle_Financeiro___Back.Data.Dtos;
using Controle_Financeiro___Back.Middleware;
using Controle_Financeiro___Back.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Controle_Financeiro___Back.Controllers;

[ApiController]
[Route("[controller]")]
public class ExpenseController : ControllerBase
{
    private IHttpContextAccessor _httpContext;
    private FinaceContext _context;
    private IMapper _mapper;
    private UserIdMiddleware _userIdMiddleware;

    public ExpenseController(FinaceContext context, IMapper mapper, IHttpContextAccessor httpContext, UserIdMiddleware userIdMiddleware)
    {
        _context = context;
        _mapper = mapper;
        _userIdMiddleware = userIdMiddleware;
    }
    [HttpPost]

    public IActionResult AddExpense([FromBody] CreateExpenseDto expenseDto)
    {
        var userId = _userIdMiddleware.GetUserId();
        var typeId = 1;
        Expense expense = _mapper.Map<Expense>(expenseDto);
        expense.TypeId = typeId;
        expense.UserId = userId;
        _context.Expenses.Add(expense);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetExpenseById),
        new { id = expense.Id },
        expense);
    }

    [HttpGet]
    public async Task<IEnumerable<ReadExpenseDto>> GetExpense([FromQuery] int take = 5, [FromQuery] int skip = 0)
    {
        var userId = _userIdMiddleware.GetUserId();
        var result = await _context.Expenses
            .Where(x => x.UserId.Equals(userId))
            .Skip(skip)
            .Take(take)
            .ToListAsync();
        return _mapper.Map<List<ReadExpenseDto>>(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetExpenseById(int id)
    {
        var expense = _context.Expenses.FirstOrDefault(expense => expense.Id == id);
        if (expense == null) return NotFound();
        return Ok(expense);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateExpense(int id, [FromBody] UpdateExpenseDto expenseDto)
    {
        var expense = _context.Expenses.FirstOrDefault(expense => expense.Id == id);
        if (expense == null) return NotFound();
        _mapper.Map(expenseDto, expense);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteExpense(int id)
    {
        var expense = _context.Expenses.FirstOrDefault(expense => expense.Id == id);
        if (expense == null) return NotFound();

        _context.Expenses.Remove(expense);
        _context.SaveChanges();

        return NoContent();
    }
}
