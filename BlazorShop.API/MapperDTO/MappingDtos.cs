using BlazorShop.API.Entities;
using BlazorShop.Model.DTOs;
using System.ComponentModel;

namespace BlazorShop.API.MapperDTO
{
    public static class MappingDtos
    {
        public static IEnumerable<CategoriaDTO> ConverterCategoriasParaDto(
                                        this IEnumerable<Categoria> categorias)
        {
            return (from categoria in categorias
                    select new CategoriaDTO
                    {
                        Id = categoria.Id,
                        Nome = categoria.Nome,
                        IconCSS = categoria.IconCSS
                    }).ToList();
        }
        public static IEnumerable<CategoriaDTO> ConverterCategoriaListParaDto(this IEnumerable<Categoria> categorias)
        {
            return categorias.Select(categoria => new CategoriaDTO
            {
                Id = categoria.Id,
                Nome = categoria.Nome,
                IconCSS = categoria.IconCSS
            }).ToList();
        }

        public static IEnumerable<ProdutoDTO> ConverterProdutoListParaDto(this IEnumerable<Produto> produtos)
        {
            return produtos.Select(produto => new ProdutoDTO
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                Quantidade = produto.Quantidade,
                ImagemUrl = produto.ImagemUrl,
                CategoriaId = produto.Categoria.Id,
                CategoriaNome = produto.Categoria.Nome  
            }).ToList();
        }

        public static CategoriaDTO ConverterCategoriaParaDto(this Categoria categoria)
        {
            return new CategoriaDTO
            {
                Id = categoria.Id,
                Nome = categoria.Nome,
                IconCSS = categoria.IconCSS
            };
        }

        public static ProdutoDTO ConverterProdutoParaDto(this Produto produto)
        {
            return new ProdutoDTO
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                Quantidade = produto.Quantidade,
                ImagemUrl = produto.ImagemUrl,
                CategoriaId = produto.Categoria.Id,
                CategoriaNome = produto.Categoria.Nome
            };
        }
        public static IEnumerable<CarrinhoItemDTO> ConverterCarrintoItemListParaDto(this IEnumerable<CarrinhoItem> carrinhoItems,
            IEnumerable<Produto> produtos)
        {
            
            return (from carrinhoItem in carrinhoItems
                    join produto in produtos
                    on carrinhoItem.ProdutoId equals produto.Id
                    select new CarrinhoItemDTO
                    {
                        Id = carrinhoItem.Id,
                        CarrinhoId = carrinhoItem.CarrinhoId,
                        ProdutoId = carrinhoItem.ProdutoId,
                        Quantidade = carrinhoItem.Quantidade,
                        ProdutoNome = produto.Nome,
                        ProdutoDescricao = produto.Descricao,
                        ProdutoImagemUrl = produto.ImagemUrl,
                        Preco = produto.Preco,
                        PrecoTotal = produto.Preco * carrinhoItem.Quantidade
                    }).ToList();
                        
        }

        public static CarrinhoItemDTO ConverterCarrinhoItemParaDTO(this CarrinhoItem carrinhoItem, Produto produto)
        {
            return new CarrinhoItemDTO
            {
                Id = carrinhoItem.Id,
                CarrinhoId = carrinhoItem.CarrinhoId,
                ProdutoId = carrinhoItem.ProdutoId,
                Quantidade = carrinhoItem.Quantidade,
                ProdutoNome = produto.Nome,
                ProdutoDescricao = produto.Descricao,
                ProdutoImagemUrl = produto.ImagemUrl,
                Preco = produto.Preco,
                PrecoTotal = produto.Preco * carrinhoItem.Quantidade
            };
        }

        public static CarrinhoDTO ConverterCarrinhoParaDTO(this Carrinho carrinho)
        {

            return new CarrinhoDTO
            {
                Id = carrinho.Id,
                UsuarioId = carrinho.UsuarioId,
                Items = carrinho.Items.Select(i => new CarrinhoItemDTO {
                    Id = i.Id,
                    CarrinhoId = i.CarrinhoId,
                    Preco = i.Produto.Preco,
                    PrecoTotal = i.Produto.Preco * i.Quantidade,
                    ProdutoDescricao = i.Produto.Descricao,
                    ProdutoId = i.ProdutoId,
                    ProdutoImagemUrl = i.Produto.ImagemUrl,
                    ProdutoNome = i.Produto.Nome,
                    Quantidade = i.Quantidade

                }).ToList()
            };
        }
    }
}
