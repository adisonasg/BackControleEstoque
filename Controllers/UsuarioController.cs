using EstoqueAPI.Data;
using EstoqueAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EstoqueAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("cadastrar")]
        public IActionResult CadastrarUsuario(UsuarioModel usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return Ok(new { mensagem = "Usuário cadastrado com sucesso" });
        }
    }
}
