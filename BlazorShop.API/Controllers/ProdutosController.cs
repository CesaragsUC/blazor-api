using BlazorShop.API.Interfaces;
using BlazorShop.API.MapperDTO;
using BlazorShop.Model.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ILogger<ProdutosController> _logger;

        public ProdutosController(IProdutoRepository produtoRepository, ILogger<ProdutosController> logger)
        {
            _produtoRepository = produtoRepository;
            _logger = logger;
        }

        [Route("all")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetAll()
        {
            try
            {
                var produto = await _produtoRepository.GetAllAsync();
                if(produto != null)
                    return Ok(produto.ConverterProdutoListParaDto());

                return NotFound();
            }
            catch (Exception ex )
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acessar a  base de dados.");
            }
        }



        [Route("obterporid/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> GetPorId(int id)
        {
            try
            {
                var produto = await _produtoRepository.GetProduto(id);
                if (produto != null)
                    return Ok(produto.ConverterProdutoParaDto());

                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acessar a  base de dados.");
            }
        }

        [Route("categoria/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> GetPorcategoria(int id)
        {
            try
            {
                var produto = await _produtoRepository.GetProdutoPorCategoriaAsync(id);
                if (produto != null)
                    return Ok(produto.ConverterProdutoListParaDto());

                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acessar a  base de dados.");
            }
        }

        [HttpGet]
        [Route("categorias")]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategorias()
        {
            try
            {
                var categorias = await _produtoRepository.GetCategorias();
                var categoriasDto = categorias.ConverterCategoriasParaDto();
                return Ok(categoriasDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                           "Erro ao acessar o banco de dados");
            }
        }

    }
}
