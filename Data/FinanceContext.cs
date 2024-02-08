using Controle_Financeiro___Back.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Controle_Financeiro___Back.Data;
public class FinaceContext : IdentityDbContext<Users>
{
    public FinaceContext(DbContextOptions<FinaceContext> opts) : base(opts) { }

    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Models.Type> Type { get; set; }
    public DbSet<Users> Users { get; set; }
}