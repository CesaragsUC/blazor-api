namespace BlazorShop.API.Entities
{
    public class Carrinho
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }

        public ICollection<CarrinhoItem> Items { get; set; } = new List<CarrinhoItem>();
    }
}
