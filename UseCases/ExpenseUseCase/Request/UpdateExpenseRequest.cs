using System.ComponentModel.DataAnnotations;
using Controle_Financeiro___Back.Data.Dtos.Services;

namespace Controle_Financeiro___Back.Data.Dtos;
public class UpdateExpenseRequest
{
    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "O valor gasto deve ser um número positivo!")]
    public double Amount { get; set; }
    public string? Description { get; set; } = "";
    [Required]
    public CreateTypeRequest Type { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
}
