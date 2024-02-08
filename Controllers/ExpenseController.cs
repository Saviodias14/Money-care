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
    private readonly ExpenseService _expenseService;
    private ErrorHandler _errorHandler;

    public ExpenseController(ExpenseService expenseService, ErrorHandler errorHandler)
    {
        _expenseService = expenseService;
        _errorHandler = errorHandler;
    }
    [HttpPost]

    public async Task<IActionResult> AddExpense([FromBody] CreateExpenseRequest expenseDto)
    {
        try
        {
            var expense = await _expenseService.CreateExpenseAsync(expenseDto);
            return CreatedAtAction(nameof(GetExpenseById),
            new { id = expense.Id },
            expense);
        }
        catch (Exception ex)
        {
            return _errorHandler.SendErrors(ex);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetExpense([FromQuery] int take = 5, [FromQuery] int skip = 0)
    {
        try
        {
            var result = await _expenseService.GetExpensesAsync(take, skip);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return _errorHandler.SendErrors(ex);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetExpenseById(int id)
    {
        try
        {
            var result = await _expenseService.GetExpenseByIdAsync(id);
            if (result is null) return NotFound("Despesa inexistente!");
            return Ok(result);
        }
        catch (Exception ex)
        {
            return _errorHandler.SendErrors(ex);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExpense(int id, [FromBody] UpdateExpenseRequest expenseDto)
    {
        try
        {
            var expense = await _expenseService.UpdateExpenseAsync(expenseDto, id);
            return Ok(expense);
        }
        catch (Exception ex)
        {
            return _errorHandler.SendErrors(ex);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExpense(int id)
    {
        try
        {
            await _expenseService.DeleteExpenseAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return _errorHandler.SendErrors(ex);
        }
    }
}
