using EstoqueAPI.Data;
using EstoqueAPI.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly TokenService _tokenService;

    public AuthController(AppDbContext context, TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

 [HttpPost("login")]
public IActionResult Login([FromBody] LoginModel login)
{
    // Buscar o usuário pelo email
    var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == login.Email);

    if (usuario == null)
    {
        return Unauthorized(new { mensagem = "Email não encontrado." });
    }

    // Verificar se a senha confere
    if (usuario.Senha != login.Senha)
    {
        return Unauthorized(new { mensagem = "Senha incorreta." });
    }

    // Gerar o token normalmente
    var token = _tokenService.GerarToken(usuario);

    Response.Cookies.Append("jwt", token, new CookieOptions
    {
        HttpOnly = true,
        Secure = false,
        SameSite = SameSiteMode.Strict,
        Expires = DateTime.UtcNow.AddHours(1)
    });

    return Ok(new 
    { 
        mensagem = "Login realizado com sucesso!",
        token = token
        });
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("jwt");
        return Ok(new { mensagem = "Logout realizado com sucesso!" });
    }

   [HttpPost("register")]
    public IActionResult Register([FromBody] LoginModel novoUsuario)
    {
        if (string.IsNullOrEmpty(novoUsuario.Email) || string.IsNullOrEmpty(novoUsuario.Senha))
        {
            return BadRequest(new { mensagem = "Email e senha são obrigatórios." });
        }

        // Verificar se já existe um usuário com esse email
        var usuarioExistente = _context.Usuarios.FirstOrDefault(u => u.Email == novoUsuario.Email);
        if (usuarioExistente != null)
        {
            return BadRequest(new { mensagem = "Email já cadastrado." });
        }

        // Criar novo usuário
        var novo = new UsuarioModel
        {
            Email = novoUsuario.Email,
            Senha = novoUsuario.Senha,
            Nome = "Usuário" // ou peça no formulário, se quiser
        };

        _context.Usuarios.Add(novo);
        _context.SaveChanges(); // salva no banco

        return Ok(new { mensagem = "Usuário cadastrado com sucesso!" });
    }
}
