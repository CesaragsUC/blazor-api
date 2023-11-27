using BlazorShop.Model.DTOs;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json;
using System.Text;

namespace Blazor.Web.Services
{
    public class CarrinhoCompraService : ICarrinhoCompraService
    {
        private readonly HttpClient httpClient;
        public CarrinhoCompraService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        //usado para atualizar qtd de itens no carrinho no header da pagina
        public event Action<int> OnCarrinhoCompraChanged;

        public async Task<CarrinhoItemDTO> AdicionaItem(CarrinhoItemAdicionarDTO carrinhoItemAdicionaDto)
        {
            try
            {
                var response = await httpClient
                              .PostAsJsonAsync<CarrinhoItemAdicionarDTO>("api/CarrinhoCompra/adicionar",
                               carrinhoItemAdicionaDto);

                if (response.IsSuccessStatusCode)// status code entre 200 a 299
                {
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        // retorna o valor "padrão" ou vazio
                        // para uma objeto do tipo carrinhoItemDto
                        return default(CarrinhoItemDTO);
                    }
                    //le o conteudo HTTP e retorna o valor resultante
                    //da serialização do conteudo JSON para o objeto Dto
                    var result =  await response.Content.ReadFromJsonAsync<CarrinhoItemDTO>();
                    return result;
                }
                else
                {
                    //serializa o conteudo HTTP como uma string
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"{response.StatusCode} Message -{message}");
                }
            }
            catch (Exception ex )
            {
                throw;
            }
        }

        public async Task<CarrinhoItemDTO> AtualizaQuantidade(CarrinhoItemAtualizarQuantidadeDTO carrinhoItemAtualizaQuantidadeDto)
        {
            try
            {
                var jsonRequest = JsonSerializer.Serialize(carrinhoItemAtualizaQuantidadeDto);

                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");

                var response = await httpClient.PatchAsync($"api/CarrinhoCompra/atualizar-quantidade/{carrinhoItemAtualizaQuantidadeDto.CarrinhoItemId}", content);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<CarrinhoItemDTO>();
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CarrinhoItemDTO> DeletaItem(int id)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"api/CarrinhoCompra/excluir/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<CarrinhoItemDTO>();
                }
                return default(CarrinhoItemDTO);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<CarrinhoItemDTO>> GetItens(string usuarioId)
        {
            try
            {
                //envia um request GET para a uri da API CarrinhoCompra
                var response = await httpClient.GetAsync($"api/CarrinhoCompra/getitens/{usuarioId}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<CarrinhoItemDTO>().ToList();
                    }
                    return await response.Content.ReadFromJsonAsync<List<CarrinhoItemDTO>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http Status Code: {response.StatusCode} Mensagem: {message}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<CarrinhoDTO> GetCarrinho(int carrinhoId)
        {
            try
            {
                //envia um request GET para a uri da API CarrinhoCompra
                var response = await httpClient.GetAsync($"api/CarrinhoCompra/carrinho/{carrinhoId}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(CarrinhoDTO);
                    }
                    return await response.Content.ReadFromJsonAsync<CarrinhoDTO>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http Status Code: {response.StatusCode} Mensagem: {message}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RaiseEventOnCarrinhoCompraChanged(int totalQuantidade)
        {
            if (OnCarrinhoCompraChanged != null)
            {
                OnCarrinhoCompraChanged.Invoke(totalQuantidade);
            }
        }
    }
}
