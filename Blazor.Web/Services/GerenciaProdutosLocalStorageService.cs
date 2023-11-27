using Blazored.LocalStorage;
using BlazorShop.Model.DTOs;

namespace Blazor.Web.Services
{
    public class GerenciaProdutosLocalStorageService : IGerenciaProdutosLocalStorageService
    {
        private const string key = "ProdutoCollection";

        private readonly ILocalStorageService localStorageService;
        private readonly IProdutoService produtoService;

        public GerenciaProdutosLocalStorageService(ILocalStorageService localStorageService,
            IProdutoService produtoService)
        {
            this.localStorageService = localStorageService;
            this.produtoService = produtoService;
        }
        public async Task<IEnumerable<ProdutoDTO>> GetCollection()
        {
            return await this.localStorageService.GetItemAsync<IEnumerable<ProdutoDTO>>(key)
                             ?? await AddCollection();
        }

        public async Task RemoveCollection()
        {
            await this.localStorageService.RemoveItemAsync(key);
        }

        private async Task<IEnumerable<ProdutoDTO>> AddCollection()
        {
            var produtoCollection = await this.produtoService.GetAll();
            if (produtoCollection != null)
            {
                await this.localStorageService.SetItemAsync(key, produtoCollection);
            }
            return produtoCollection;
        }
    }
}
