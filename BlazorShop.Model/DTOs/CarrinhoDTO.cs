namespace BlazorShop.Model.DTOs
{
    public class CarrinhoDTO
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }

        public decimal PrecoTotal { get; set; }

        public ICollection<CarrinhoItemDTO> Items { get; set; } = new List<CarrinhoItemDTO>();
    }

}
