using BlazorShop.Model.DTOs;

namespace Blazor.Web.Services
{
    public interface IGerenciaCarrinhoItensLocalStorageService
    {
        Task<List<CarrinhoItemDTO>> GetCollection();
        Task SaveCollection(List<CarrinhoItemDTO> carrinhoItensDto);
        Task RemoveCollection();
    }
}
