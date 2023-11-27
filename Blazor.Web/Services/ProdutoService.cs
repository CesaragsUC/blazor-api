using BlazorShop.Model.DTOs;
using System.Net.Http.Json;

namespace Blazor.Web.Services
{
    public class ProdutoService : IProdutoService
    {
        public HttpClient _httpClient;
       private ILogger<ProdutoService> _logger;
        public ProdutoService(HttpClient httpClient , ILogger<ProdutoService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<ProdutoDTO> Get(int id)
        {
            try
            {
                var produtoDTO = await _httpClient.GetFromJsonAsync<ProdutoDTO>($"api/produtos/obterporid/{id}");
                return produtoDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao acessar end-point: api/produtos/all");
                return null;
            }
        }

        public async Task<IEnumerable<ProdutoDTO>> GetAll()
        {
            try
            {
                var produtoDTO = await _httpClient.GetFromJsonAsync<IEnumerable<ProdutoDTO>>("api/produtos/all");
                return produtoDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao acessar end-point: api/produtos/all");
                return null;
            }

        }

        public async Task<IEnumerable<CategoriaDTO>> GetCategorias()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Produtos/categorias");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<CategoriaDTO>();
                    }
                    return await response.Content.ReadFromJsonAsync<IEnumerable<CategoriaDTO>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http Status Code - {response.StatusCode} Message - {message}");
                }
            }
            catch (Exception)
            {
                //log exception
                throw;
            }
        }

        public async Task<IEnumerable<ProdutoDTO>> GetItensPorCategoria(int categoriaId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Produtos/categoria/{categoriaId}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ProdutoDTO>();
                    }
                    return await response.Content.ReadFromJsonAsync<IEnumerable<ProdutoDTO>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http Status Code - {response.StatusCode} Message - {message}");
                }
            }
            catch (Exception)
            {
                //log exception
                throw;
            }
        }
    }
}
