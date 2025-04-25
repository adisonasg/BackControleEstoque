using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly TokenService _tokenService;

    public AuthController(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel login)
    {
        // Aqui você pode usar EF para buscar do banco
        if (login.Email == "admin@teste.com" && login.Senha == "123456")
        {
            var usuario = new UsuarioModel
            {
                Id = 1,
                Email = login.Email,
                Nome = "Administrador"
            };

            var token = _tokenService.GerarToken(usuario);

            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(1)
            });

            return Ok(new { mensagem = "Login realizado com sucesso!" });
        }

        return Unauthorized();
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("jwt");
        return Ok(new { mensagem = "Logout realizado com sucesso!" });
    }
}
