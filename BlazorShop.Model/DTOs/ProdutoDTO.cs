using System.ComponentModel.DataAnnotations;

namespace BlazorShop.Model.DTOs
{
    public class ProdutoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;


        public string Descricao { get; set; } = string.Empty;

        public string ImagemUrl { get; set; } = string.Empty;
        public decimal Preco { get; set; }

        public int Quantidade { get; set; }

        public int CategoriaId { get; set; }
        public string CategoriaNome { get; set; } = string.Empty;

    }

}
