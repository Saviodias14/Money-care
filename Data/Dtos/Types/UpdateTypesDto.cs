using System.ComponentModel.DataAnnotations;

namespace Controle_Financeiro___Back.Data.Dtos.Services;
public class UpdateTypeDto
{
    [Required]
    public string Name { get; set; }
}
