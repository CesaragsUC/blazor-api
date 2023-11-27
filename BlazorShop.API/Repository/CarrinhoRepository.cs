using BlazorShop.API.Context;
using BlazorShop.API.Entities;
using BlazorShop.API.Interfaces;
using BlazorShop.Model.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BlazorShop.API.Repository
{
    public class CarrinhoCompraRepository : ICarrinhoCompraRepository
    {
        private readonly ShopContext _context;
        public CarrinhoCompraRepository(ShopContext context)
        {
            _context = context;
        }

        private async Task<bool> ItemJaExisteNocarrinho(int carrihoId, int produtoId)
        {
            return await _context.CarrinhoItems.AnyAsync(c => c.CarrinhoId == carrihoId && c.ProdutoId == produtoId);
        }

        private async Task<bool> CarrinhoJaExiste(int id)
        {
            return await _context.Carrinhos.AnyAsync(c => c.UsuarioId == id);
        }

        private async Task<Carrinho> ObterCarrinhoPorUsuario(int id)
        {
            return await _context.Carrinhos.FirstAsync(c => c.UsuarioId == id);
        }

        private async Task<Carrinho> CriarCarrinho(int usuario)
        {
            var carrinho = new Carrinho
            {
                UsuarioId = usuario
            };
            var result  = await _context.Carrinhos.AddAsync(carrinho);
            await _context.SaveChangesAsync();
            return result.Entity;
        }


        private async Task Excluir(int carrinhoId)
        {
            var carrinhoitem = await _context.CarrinhoItems.Where(c => c.Id == carrinhoId).ToListAsync();
            if (carrinhoitem .Any())
            {
                foreach (var item in carrinhoitem)
                {
                    _context.CarrinhoItems.Remove(item);
                    await _context.SaveChangesAsync();
                }

            }

            var carrinho = await _context.Carrinhos.FirstAsync(c => c.Id == carrinhoId);
            if (carrinho != null)
            {
                _context.Carrinhos.Remove(carrinho);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<CarrinhoItem> AddItem(CarrinhoItemAdicionarDTO itemCarrinho)
        {

            if(!await CarrinhoJaExiste(itemCarrinho.CarrinhoId))
            {
              var _carrinho =  await CriarCarrinho(1); // passando fixo, mas poderia passar id do usuario logado.
                itemCarrinho.CarrinhoId = _carrinho.Id;
            }
            else
            {
                var carrinho = await ObterCarrinhoPorUsuario(1);// passando fixo, mas poderia passar id do usuario logado.
                itemCarrinho.CarrinhoId = carrinho.Id;
            }


            if (!await ItemJaExisteNocarrinho(itemCarrinho.CarrinhoId, itemCarrinho.ProdutoId))
            {
                //verifica se o produto existe e ja cria um novo item no carrinho
                var item = await (from produto in _context.Produtos
                                  where produto.Id == itemCarrinho.ProdutoId
                                  select new CarrinhoItem
                                  {
                                      CarrinhoId = itemCarrinho.CarrinhoId,
                                      ProdutoId = produto.Id,
                                      Quantidade = itemCarrinho.Quantidade,
                                  }).FirstOrDefaultAsync();

                if (item != null)
                {
                    try
                    {
                        var result = await _context.CarrinhoItems.AddAsync(item);
                        await _context.SaveChangesAsync();
                        return result.Entity;
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }


                }
            }

            return null;
        }

        public async Task<CarrinhoItem> AtualizarQuantidade(int id, CarrinhoItemAtualizarQuantidadeDTO itemCarrinho)
        {
            var carrinhoItem = await _context.CarrinhoItems.FindAsync(id);

            if (carrinhoItem != null)
            {
                carrinhoItem.Quantidade = itemCarrinho.Quantidade;
                await _context.SaveChangesAsync();
                return carrinhoItem;
            }
            return null;
        }

        public async Task<CarrinhoItem> DeletaItem(int id)
        {
            var item = await _context.CarrinhoItems.FindAsync(id);
            if (item != null)
            {
                _context.CarrinhoItems.Remove(item);
                await _context.SaveChangesAsync();

            }
            return item;
        }

        public async Task<CarrinhoItem> GetItem(int id)
        {
            var result = await (from carrinho in _context.Carrinhos
                                join carrinhoItem in _context.CarrinhoItems
                                on carrinho.Id equals carrinhoItem.CarrinhoId
                                where carrinhoItem.Id == id
                                select new CarrinhoItem
                                {

                                    Id = carrinhoItem.Id,
                                    ProdutoId = carrinhoItem.ProdutoId,
                                    Quantidade = carrinhoItem.Quantidade,
                                    CarrinhoId = carrinhoItem.CarrinhoId,
                                }).FirstOrDefaultAsync();

            return result;
        }

        public async Task<IEnumerable<CarrinhoItem>> GetItens(int usuarioId)
        {
            var result = await (from carrinho in _context.Carrinhos
                                join carrinhoItem in _context.CarrinhoItems
                                on carrinho.Id equals carrinhoItem.CarrinhoId
                                where carrinho.UsuarioId == usuarioId
                                select new CarrinhoItem
                                {

                                    Id = carrinhoItem.Id,
                                    ProdutoId = carrinhoItem.ProdutoId,
                                    Quantidade = carrinhoItem.Quantidade,
                                    CarrinhoId = carrinhoItem.CarrinhoId,
                                }).ToListAsync();

            return result;
        }

        public async Task<Carrinho> ObterCarrinhoPorId(int carrinhoId)
        {
            var result = await _context.Carrinhos
                .Include(i => i.Items)
                .ThenInclude(p => p.Produto)
                .FirstOrDefaultAsync(c => c.Id == carrinhoId);
            return result;
        }


    }
}
