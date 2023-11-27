using System.ComponentModel.DataAnnotations;

namespace BlazorShop.Model.DTOs
{
    public class CarrinhoItemAdicionarDTO
    {
        [Required]
        public int CarrinhoId { get; set; }
        [Required]
        public int ProdutoId { get; set; }
        [Required]
        public int Quantidade { get; set; }
    }

}
