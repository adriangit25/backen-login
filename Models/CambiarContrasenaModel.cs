using System.ComponentModel.DataAnnotations;

public class CambiarContrasenaModel
{
    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "Formato de correo inválido.")]
    public string? Correo { get; set; }

    [Required(ErrorMessage = "La nueva contraseña es obligatoria.")]
    [StringLength(50, MinimumLength = 6, ErrorMessage = "La nueva contraseña debe tener entre 6 y 50 caracteres.")]
    public string? NuevaContrasena { get; set; }

    [Required(ErrorMessage = "Debe confirmar la nueva contraseña.")]
    [Compare("NuevaContrasena", ErrorMessage = "Las contraseñas no coinciden.")]
    public string? ConfirmarContrasena { get; set; }
}
