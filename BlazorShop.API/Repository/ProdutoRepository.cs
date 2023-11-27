using BlazorShop.API.Context;
using BlazorShop.API.Entities;
using BlazorShop.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlazorShop.API.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ShopContext _context;
        public ProdutoRepository(ShopContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produto>> GetAllAsync()
        {
            return await _context.Produtos.Include(c => c.Categoria).ToListAsync();
        }

        public async Task<IEnumerable<Categoria>> GetCategorias()
        {

            var categorias = await _context.Categorias.ToListAsync();
            return categorias;
        }

        public async Task<Produto> GetProduto(int id)
        {
            return await _context.Produtos.Include(c => c.Categoria).FirstAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Produto>> GetProdutoPorCategoriaAsync(int id)
        {
            return await _context.Produtos.Include(c => c.Categoria)
                .Where(x => x.CategoriaId == id).ToListAsync();
        }
    }
}
