using BlazorShop.API.Entities;
using BlazorShop.Model.DTOs;

namespace BlazorShop.API.Interfaces
{
    public interface ICarrinhoCompraRepository
    {
        Task<Carrinho> ObterCarrinhoPorId(int carrinhoId);
        Task<IEnumerable<CarrinhoItem>> GetItens(int usuarioId);
        Task<CarrinhoItem> GetItem(int id);
        Task<CarrinhoItem> DeletaItem(int id);
        Task<CarrinhoItem> AddItem(CarrinhoItemAdicionarDTO item);
        Task<CarrinhoItem> AtualizarQuantidade(int id , CarrinhoItemAtualizarQuantidadeDTO item);
    }

}
