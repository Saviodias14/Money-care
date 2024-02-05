using System.ComponentModel.DataAnnotations;

namespace Controle_Financeiro___Back.Data.Dtos;
public class UpdateExpenseDto
{
    [Required(ErrorMessage = "O campo 'Amount' é obrigatório.")]
    [Range(0, double.MaxValue, ErrorMessage = "O valor gasto não pode ser negativo!")]
    public double Amount { get; set; }

    [Required(ErrorMessage = "O campo 'Description' é obrigatório.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "O campo 'ServiceId' é obrigatório.")]
    public int ServiceId { get; set; }

    public DateTime Date { get; set; } = DateTime.Now;
}
