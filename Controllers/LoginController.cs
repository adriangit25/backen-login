using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Login.Data;
using Login.Models;

namespace Login.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if (loginModel == null || string.IsNullOrEmpty(loginModel.Correo) || string.IsNullOrEmpty(loginModel.Contrasenia))
            {
                return BadRequest("Por favor, ingrese correo y contraseña.");
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.UsuCorreo == loginModel.Correo);

            if (usuario == null)
            {
                return Unauthorized("Correo no encontrado.");
            }

            if (usuario.UsuContrasenia?.Trim() != loginModel.Contrasenia?.Trim())
            {
                return Unauthorized("Contraseña incorrecta.");
            }

            return Ok(new { mensaje = "Inicio de sesión exitoso", usuario = usuario });
        }

    }
}
