using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EstoqueAPI.Models;
using EstoqueAPI.Services;


namespace EstoqueAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProdutosController : ControllerBase
    {
        private readonly ProdutoService _produtoService;

        public ProdutosController(ProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> GetTodos()
        {
            return Ok(_produtoService.GetTodos());
        }

        [HttpGet("{id}")]
        public ActionResult<Produto> GetPorId(int id)
        {
            var produto = _produtoService.GetPorId(id);
            if (produto == null)
                return NotFound();

            return Ok(produto);
        }

        [HttpPost]
        public ActionResult<Produto> Criar([FromBody] Produto novoProduto)
        {
            var criado = _produtoService.Criar(novoProduto);
            return CreatedAtAction(nameof(GetTodos), new { id = criado.Id }, criado);
        }

        [HttpPut("{id}")]
        public ActionResult Atualizar(int id, [FromBody] Produto produtoAtualizado)
        {
            var sucesso = _produtoService.Atualizar(id, produtoAtualizado);
            if (!sucesso)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Remover(int id)
        {
            var sucesso = _produtoService.Remover(id);
            if (!sucesso)
                return NotFound();

            return NoContent();
        }
    }
}
