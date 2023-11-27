using BlazorShop.API.Interfaces;
using BlazorShop.API.MapperDTO;
using BlazorShop.Model.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlazorShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrinhoCompraController : ControllerBase
    {
        private readonly ICarrinhoCompraRepository _carrinhoItemRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly ILogger<CarrinhoCompraController> _logger;

        public CarrinhoCompraController(IProdutoRepository produtoRepository,
            ILogger<CarrinhoCompraController> logger,
            ICarrinhoCompraRepository carrinhoCompraRepository)
        {
            _produtoRepository = produtoRepository;
            _logger = logger;
            _carrinhoItemRepository = carrinhoCompraRepository;
        }

        [Route("getitens/{usuarioId}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarrinhoItemDTO>>> GetItens(string usuarioId)
        {
            try
            {
                var carrinhoItens = await _carrinhoItemRepository.GetItens(Convert.ToInt32(usuarioId));
                if (carrinhoItens == null)
                    return NoContent();  // 204 Status Code

                var produtos = await _produtoRepository.GetAllAsync();
                if (produtos == null)
                    return NoContent();  // 204 Status Code

                var dto = carrinhoItens.ConverterCarrintoItemListParaDto(produtos);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError("## Erro ao obter itens do carrinho");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acessar a  base de dados.");
            }
        }

        [Route("carrinho/{id:int}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarrinhoDTO>>> GetCarrinho(int id)
        {
            try
            {
                var carrinho = await _carrinhoItemRepository.ObterCarrinhoPorId(id);
                if (carrinho == null)
                    return NoContent();  // 204 Status Code

                var dto = carrinho.ConverterCarrinhoParaDTO();
                return Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError("## Erro ao obter o carrinho");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acessar a  base de dados.");
            }
        }

        [Route("item/{id:int}")]
        [HttpGet]
        public async Task<ActionResult<CarrinhoItemDTO>> GetItem(int id)
        {
            try
            {
                var carrinhoItem = await _carrinhoItemRepository.GetItem(id);
                if (carrinhoItem == null)
                {
                    return NotFound($"Item não encontrado"); //404 status code
                }

                var produto = await _produtoRepository.GetProduto(carrinhoItem.ProdutoId);

                if (produto == null)
                {
                    return NotFound($"Item não existe na fonte de dados");
                }
                var cartItemDto = carrinhoItem.ConverterCarrinhoItemParaDTO(produto);

                return Ok(cartItemDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"## Erro ao obter o item ={id} do carrinho");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Route("adicionar")]
        [HttpPost]
        public async Task<ActionResult<CarrinhoItemDTO>> PostItem([FromBody] CarrinhoItemAdicionarDTO carrinhoItemAdicionaDto)
        {
            try
            {
                var novoCarrinhoItem = await _carrinhoItemRepository.AddItem(carrinhoItemAdicionaDto);

                if (novoCarrinhoItem == null)
                {
                    return NoContent(); //Status 204
                }

                var produto = await _produtoRepository.GetProduto(novoCarrinhoItem.ProdutoId);

                if (produto == null)
                {
                    throw new Exception($"Produto não localizado (Id:({carrinhoItemAdicionaDto.ProdutoId})");
                }

                var novoCarrinhoItemDto = novoCarrinhoItem.ConverterCarrinhoItemParaDTO(produto);

                return CreatedAtAction(nameof(GetItem), new { id = novoCarrinhoItemDto.Id },
                    novoCarrinhoItemDto);

            }
            catch (Exception ex)
            {
                _logger.LogError("## Erro criar um novo item no carrinho");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Route("excluir/{id:int}")]
        [HttpDelete]
        public async Task<ActionResult<CarrinhoItemDTO>> DeleteItem(int id)
        {
            try
            {
                var carrinhoItem = await _carrinhoItemRepository.DeletaItem(id);

                if (carrinhoItem == null)
                {
                    return NotFound();
                }

                var produto = await _produtoRepository.GetProduto(carrinhoItem.ProdutoId);

                if (produto is null)
                    return NotFound();

                var carrinhoItemDto = carrinhoItem.ConverterCarrinhoItemParaDTO(produto);
                return Ok(carrinhoItemDto);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Route("atualizar-quantidade/{id:int}")]
        [HttpPatch]
        public async Task<ActionResult<CarrinhoItemDTO>> AtualizaQuantidade(int id,
            CarrinhoItemAtualizarQuantidadeDTO carrinhoItemAtualizaQuantidadeDto)
        {
            try
            {

                var carrinhoItem = await _carrinhoItemRepository.AtualizarQuantidade(id,
                                       carrinhoItemAtualizaQuantidadeDto);

                if (carrinhoItem == null)
                {
                    return NotFound();
                }
                var produto = await _produtoRepository.GetProduto(carrinhoItem.ProdutoId);
                var carrinhoItemDto = carrinhoItem.ConverterCarrinhoItemParaDTO(produto);
                return Ok(carrinhoItemDto);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
