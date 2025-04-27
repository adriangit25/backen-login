namespace Login.Models
{
    public class UsuarioRegistroDTO
    {
        public string UsuUsuario { get; set; }
        public string UsuCorreo { get; set; }
        public string UsuContrasenia { get; set; }
        
        public string RegNombre { get; set; }
        public string RegApellido { get; set; }
        public DateTime RegFechaNacim { get; set; }
        public string RegTelefono { get; set; }
    }
}
