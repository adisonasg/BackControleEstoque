using Microsoft.AspNetCore.Mvc;
using EstoqueAPI.Models;

namespace EstoqueAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private static List<Produto> produtos = new List<Produto>
        {
            new Produto { Id = 1, Nome = "Mouse", Descricao = "Mouse óptico", Quantidade = 50, Preco = 89.90m },
            new Produto { Id = 2, Nome = "Teclado", Descricao = "Teclado mecânico", Quantidade = 20, Preco = 199.90m }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> GetTodos()
        {
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public ActionResult<Produto> GetPorId(int id)
        {
            var produto = produtos.FirstOrDefault(p => p.Id == id);
            if (produto == null) return NotFound();
            return Ok(produto);
        }

        [HttpPost]
        public ActionResult<Produto> Criar([FromBody] Produto novoProduto)
        {
            novoProduto.Id = produtos.Max(p => p.Id) + 1;
            produtos.Add(novoProduto);
            return CreatedAtAction(nameof(GetPorId), new { id = novoProduto.Id }, novoProduto);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] Produto produtoAtualizado)
        {
            var produto = produtos.FirstOrDefault(p => p.Id == id);
            if (produto == null) return NotFound();

            produto.Nome = produtoAtualizado.Nome;
            produto.Descricao = produtoAtualizado.Descricao;
            produto.Quantidade = produtoAtualizado.Quantidade;
            produto.Preco = produtoAtualizado.Preco;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var produto = produtos.FirstOrDefault(p => p.Id == id);
            if (produto == null) return NotFound();

            produtos.Remove(produto);
            return NoContent();
        }
    }
}
