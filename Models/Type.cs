using System.ComponentModel.DataAnnotations;

namespace Controle_Financeiro___Back.Models;

public class Type
{
    [Key]
    [Required]
    public int Id { get; internal set; }
    [Required]
    public string Name { get; set; }

    // public virtual ICollection<Expense> Expenses { get; set; }

}
