using System.ComponentModel.DataAnnotations;

namespace Controle_Financeiro___Back.Data.Dtos;
public class CreateExpenseDto
{
    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "O valor gasto não pode ser negativo!")]
    public double Amount { get; set; }
    [Required]
    public string? Service { get; set; }
    [Required]
    public DateTime Date { get; set; }
}
