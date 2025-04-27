using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class LoginModel
{
    public string? Usuario { get; set; }
    public string? Correo { get; set; }
    public string? Contrasenia { get; set; }
}
