using BlazorShop.Model.DTOs;

namespace Blazor.Web.Services
{
    public interface IProdutoService
    {
        Task<IEnumerable<ProdutoDTO>> GetAll();
        Task<ProdutoDTO> Get(int id);


        Task<IEnumerable<CategoriaDTO>> GetCategorias();
        Task<IEnumerable<ProdutoDTO>> GetItensPorCategoria(int categoriaId);
    }
}
