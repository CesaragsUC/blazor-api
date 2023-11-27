using Blazored.LocalStorage;
using BlazorShop.Model.DTOs;

namespace Blazor.Web.Services
{
    public class GerenciaCarrinhoItensLocalStorageService : IGerenciaCarrinhoItensLocalStorageService
    {
        const string key = "CarrinhoItemCollection";

        private readonly ILocalStorageService localStorageService;
        private readonly ICarrinhoCompraService carrinhoCompraService;

        public GerenciaCarrinhoItensLocalStorageService(ILocalStorageService localStorageService,
            ICarrinhoCompraService carrinhoCompraService)
        {
            this.localStorageService = localStorageService;
            this.carrinhoCompraService = carrinhoCompraService;
        }

        public async Task<List<CarrinhoItemDTO>> GetCollection()
        {
            return await this.localStorageService.GetItemAsync<List<CarrinhoItemDTO>>(key)
                   ?? await AddCollection();
        }

        public async Task RemoveCollection()
        {
            await this.localStorageService.RemoveItemAsync(key);
        }

        public async Task SaveCollection(List<CarrinhoItemDTO> carrinhoItensDto)
        {
            await this.localStorageService.SetItemAsync(key, carrinhoItensDto);
        }

        //obtem os dados do servidor e armazena no localstorage
        private async Task<List<CarrinhoItemDTO>> AddCollection()
        {
            var carrinhoCompraCollection = await this.carrinhoCompraService
                                              .GetItens(UsuarioLogado.UsuarioId);

            if (carrinhoCompraCollection != null)
            {
                await this.localStorageService.SetItemAsync(key, carrinhoCompraCollection);
            }
            return carrinhoCompraCollection;
        }
    }
}
