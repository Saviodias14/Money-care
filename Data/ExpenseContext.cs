
using Controle_Financeiro___Back.Models;
using Microsoft.EntityFrameworkCore;

namespace Controle_Financeiro___Back.Data;
public class ExpenseContext : DbContext
{
    public ExpenseContext(DbContextOptions<ExpenseContext> opts)
    : base(opts)
    {

    }

    public DbSet<Expense> expenses { get; set; }
}