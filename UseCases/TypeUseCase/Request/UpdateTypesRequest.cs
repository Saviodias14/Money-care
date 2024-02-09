using System.ComponentModel.DataAnnotations;

namespace Controle_Financeiro___Back.Data.Dtos.Services;
public class UpdateTypeRequest
{
    [Required]
    public string Name { get; set; }
}
