using System.ComponentModel.DataAnnotations;

namespace Controle_Financeiro___Back.Data.Dtos;
public class CreateUserDto
{
    [Required]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
    public string Username { get; set; }
    [Required]
    [EmailAddress(ErrorMessage = "O campo precisa ser um endereço de e-mail válido.")]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [StringLength(50, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 50 caracteres.")]
    public string Password { get; set; }
    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}
