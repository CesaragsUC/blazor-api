using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace BlazorShop.API.Entities
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Descricao { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string ImagemUrl { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        
        public int Quantidade { get; set; }    

        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }

        public ICollection<CarrinhoItem> Items { get; set; } = new List<CarrinhoItem>();


    }
}
