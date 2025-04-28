using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Login.Data;
using Login.Models;

namespace Login.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/registros
        [HttpGet("registros")]
        public async Task<ActionResult<IEnumerable<Registro>>> GetRegistros()
        {
            var registros = await _context.Registros.ToListAsync(); // Obtener todos los registros
            return Ok(registros); // Retornar los registros
        }


        // POST: api/usuarios
        [HttpPost("crearUsuario")]
        public async Task<ActionResult<Usuario>> CrearUsuario([FromBody] Usuario nuevoUsuario)
        {
            if (nuevoUsuario == null)
            {
                return BadRequest("Datos de usuario inválidos.");
            }

            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuarios), new { id = nuevoUsuario.UsuId }, nuevoUsuario);
        }

        // POST: api/registros
        [HttpPost("registro")]
        public async Task<IActionResult> CrearRegistro([FromBody] Registro nuevoRegistro)
        {
            if (nuevoRegistro == null)
            {
                return BadRequest("Datos inválidos.");
            }

            _context.Registros.Add(nuevoRegistro);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRegistros), new { id = nuevoRegistro.RegId }, nuevoRegistro);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            // Validación de los datos entrantes
            if (loginModel == null || string.IsNullOrEmpty(loginModel.Correo) || string.IsNullOrEmpty(loginModel.Contrasenia))
            {
                return BadRequest("Por favor, ingrese correo y contraseña.");
            }

            // Buscar al usuario por correo electrónico
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.UsuCorreo == loginModel.Correo);

            if (usuario == null)
            {
                return Unauthorized("Correo no encontrado.");
            }

            // Log de las contraseñas para depuración (seguro)
            Console.WriteLine("Contraseña ingresada: '" + loginModel.Contrasenia + "' (tamaño: " + loginModel.Contrasenia.Length + ")");
            Console.WriteLine("Contraseña almacenada: '" + usuario.UsuContrasenia + "' (tamaño: " + (usuario.UsuContrasenia?.Length ?? 0) + ")");

            // Verificar la contraseña (seguro con ?.Trim())
            if (usuario.UsuContrasenia?.Trim() != loginModel.Contrasenia?.Trim()) // Comparación sin espacios
            {
                return Unauthorized("Contraseña incorrecta.");
            }

            // Si todo es correcto, devolver un mensaje de éxito
            return Ok(new { mensaje = "Inicio de sesión exitoso", usuario = usuario });
        }


        [HttpPut("cambiarContrasena")]
        public async Task<IActionResult> CambiarContrasena([FromBody] CambiarContrasenaModel cambiarModel)
        {
            // Validación de los datos
            if (cambiarModel == null || string.IsNullOrEmpty(cambiarModel.Correo) || string.IsNullOrEmpty(cambiarModel.NuevaContrasena) || string.IsNullOrEmpty(cambiarModel.ConfirmarContrasena))
            {
                return BadRequest("Por favor, ingrese todos los datos correctamente.");
            }

            // Verificar que las contraseñas coincidan
            if (cambiarModel.NuevaContrasena != cambiarModel.ConfirmarContrasena)
            {
                return BadRequest("Las contraseñas no coinciden.");
            }

            // Buscar al usuario por correo
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.UsuCorreo == cambiarModel.Correo);

            if (usuario == null)
            {
                return Unauthorized("Correo no encontrado.");
            }

            // Actualizar la contraseña del usuario
            usuario.UsuContrasenia = cambiarModel.NuevaContrasena; // Aquí podrías aplicar un hash de contraseña si lo deseas en el futuro

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Contraseña cambiada con éxito." });
        }

    }
}
