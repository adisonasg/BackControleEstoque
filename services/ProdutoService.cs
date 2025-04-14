using EstoqueAPI.Models;
using EstoqueAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace EstoqueAPI.Services
{
    public class ProdutoService
    {
        private readonly AppDbContext _context;

        public ProdutoService(AppDbContext context)
        {
            _context = context;
        }

        public List<Produto> GetTodos()
        {
            return _context.Produtos.ToList();
        }

        public Produto? GetPorId(int id)
        {
            return _context.Produtos.Find(id);
        }

        public Produto Criar(Produto novoProduto)
        {
            _context.Produtos.Add(novoProduto);
            _context.SaveChanges();
            return novoProduto;
        }

        public bool Atualizar(int id, Produto produtoAtualizado)
        {
            var produto = _context.Produtos.Find(id);
            if (produto == null)
                return false;

            produto.Nome = produtoAtualizado.Nome;
            produto.Descricao = produtoAtualizado.Descricao;
            produto.Quantidade = produtoAtualizado.Quantidade;
            produto.Preco = produtoAtualizado.Preco;

            _context.SaveChanges();
            return true;
        }

        public bool Remover(int id)
        {
            var produto = _context.Produtos.Find(id);
            if (produto == null)
                return false;

            _context.Produtos.Remove(produto);
            _context.SaveChanges();
            return true;
        }
    }
}
