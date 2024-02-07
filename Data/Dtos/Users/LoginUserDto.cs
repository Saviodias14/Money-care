using System.ComponentModel.DataAnnotations;

namespace Controle_Financeiro___Back.Data.Dtos.Users;
public class LoginUserDto
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    public void Deconstruct(out string username, out string password)
    {
        username = Username;
        password = Password;
    }
}
