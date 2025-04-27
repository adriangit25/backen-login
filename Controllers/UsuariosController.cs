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

        // POST: api/usuarios
        [HttpPost]
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

        // POST: api/usuarios/registro
        [HttpPost("registro")]
        public async Task<IActionResult> CrearUsuarioConRegistro([FromBody] UsuarioRegistroDTO datos)
        {
            if (datos == null)
            {
                return BadRequest("Datos inválidos.");
            }

            // Opcional: usar una transacción para que ambos inserts sean atómicos
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var usuario = new Usuario
                {
                    UsuUsuario = datos.UsuUsuario,
                    UsuCorreo = datos.UsuCorreo,
                    UsuContrasenia = datos.UsuContrasenia,
                    UsuEstado = 1
                };

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                var registro = new Registro
                {
                    RegNombre = datos.RegNombre,
                    RegApellido = datos.RegApellido,
                    RegFechaNacim = datos.RegFechaNacim,
                    RegTelefono = datos.RegTelefono,
                    RegEstado = 1
                };

                _context.Registros.Add(registro);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return Created("", new { usuario, registro });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

    }
}
