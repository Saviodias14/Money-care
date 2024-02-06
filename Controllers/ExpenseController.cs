using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using Controle_Financeiro___Back.Data;
using Controle_Financeiro___Back.Data.Dtos;
using Controle_Financeiro___Back.Models;
using Microsoft.AspNetCore.Mvc;

namespace Controle_Financeiro___Back.Controllers;

[ApiController]
[Route("[controller]")]
public class ExpenseController : ControllerBase
{
    private FinaceContext _context;
    private IMapper _mapper;

    public ExpenseController(FinaceContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    [HttpPost]
    public IActionResult AddExpense([FromBody] CreateExpenseDto expenseDto)
    {
        Expense expense = _mapper.Map<Expense>(expenseDto);
        _context.Expenses.Add(expense);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetExpenseById),
        new { id = expense.Id },
        expense);
    }

    [HttpGet]
    public IEnumerable<Expense> GetExpense([FromQuery] int take = 5, [FromQuery] int skip = 0)
    {
        return _context.Expenses.Skip(skip).Take(take);
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
