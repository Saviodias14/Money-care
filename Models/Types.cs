using System.ComponentModel.DataAnnotations;

namespace Controle_Financeiro___Back.Models;

public class Types
{
    [Key]
    [Required]
    public int Id { get; internal set; }
    [Required]
    public string Name { get; set; }

    public virtual ICollection<Expense> Expenses { get; set; }

}
