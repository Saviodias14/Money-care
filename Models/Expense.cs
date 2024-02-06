using System.ComponentModel.DataAnnotations;

namespace Controle_Financeiro___Back.Models;
public class Expense
{
    [Key]
    [Required]
    public int Id { get; internal set; }
    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "O valor gasto não pode ser negativo!")]
    public double Amount { get; set; }
    [Required]
    public int UserId { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public int TypeId { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public virtual Users User { get; set; }
    public virtual Types Types { get; set; }
}
