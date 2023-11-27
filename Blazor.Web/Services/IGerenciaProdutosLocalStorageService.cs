using BlazorShop.Model.DTOs;

namespace Blazor.Web.Services
{
    public interface IGerenciaProdutosLocalStorageService
    {
        Task<IEnumerable<ProdutoDTO>> GetCollection();
        Task RemoveCollection();
    }
}
