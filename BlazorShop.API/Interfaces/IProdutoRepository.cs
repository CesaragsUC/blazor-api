using BlazorShop.API.Entities;

namespace BlazorShop.API.Interfaces
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> GetAllAsync();
        Task<Produto> GetProduto(int id);

        Task<IEnumerable<Produto>> GetProdutoPorCategoriaAsync(int id);
        Task<IEnumerable<Categoria>> GetCategorias();
    }

}
