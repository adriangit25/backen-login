using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Login.Models
{
    [Table("tbl_usuarios")]
    public class Usuario
    {
        [Key]
        [Column("usu_id")]
        public int UsuId { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [Column("usu_usuario")]
        public string? UsuUsuario { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido.")]
        [Column("usu_correo")]
        public string? UsuCorreo { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 50 caracteres.")]
        [Column("usu_contrasenia")]
        public string? UsuContrasenia { get; set; }

        [Column("usu_estado")]
        public int UsuEstado { get; set; } = 1;
    }
}
