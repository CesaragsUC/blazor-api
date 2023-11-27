using BlazorShop.Model.DTOs;

namespace Blazor.Web.Services
{
    public interface ICarrinhoCompraService
    {
        Task<CarrinhoDTO> GetCarrinho(int carrinhoId);
        Task<List<CarrinhoItemDTO>> GetItens(string usuarioId);
        Task<CarrinhoItemDTO> AdicionaItem(CarrinhoItemAdicionarDTO carrinhoItemAdicionaDto);
        Task<CarrinhoItemDTO> DeletaItem(int id);
        Task<CarrinhoItemDTO> AtualizaQuantidade(CarrinhoItemAtualizarQuantidadeDTO carrinhoItemAtualizaQuantidadeDto);

        event Action<int> OnCarrinhoCompraChanged;
        void RaiseEventOnCarrinhoCompraChanged(int totalQuantidade);
    }
}
