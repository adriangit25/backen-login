using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Login.Models
{
    [Table("tbl_registro")]
    public class Registro
    {
        [Key]
        [Column("reg_id")]
        public int RegId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [Column("reg_nombre")]
        public string? RegNombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [Column("reg_apellido")]
        public string? RegApellido { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [Phone(ErrorMessage = "El teléfono debe ser válido.")]
        [Column("reg_telefono")]
        public string? RegTelefono { get; set; }

        [Column("reg_estado")]
        public int RegEstado { get; set; } = 1;

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        [Column("reg_fechaNacimiento")]
        public string? RegFechaNacimiento { get; set; }
    }
}
