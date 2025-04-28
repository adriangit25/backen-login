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


    }
}
