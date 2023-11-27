using BlazorShop.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorShop.API.Context
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {

        }

        public DbSet<Carrinho>? Carrinhos { get; set; }
        public DbSet<CarrinhoItem>? CarrinhoItems { get; set; }
        public DbSet<Categoria>? Categorias { get; set; }
        public DbSet<Produto>? Produtos { get; set; }
        public DbSet<Usuario>? Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetProperties()
                .Where(p => p.ClrType == typeof(decimal))))
                        property.SetColumnType("decimal(10,2)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShopContext).Assembly);


            modelBuilder.Entity<Categoria>()
                .HasData(
                new Categoria { Id = 1, Nome = "Celulares", IconCSS = "fa-mobile-alt" },
                new Categoria { Id = 2, Nome = "Computadores", IconCSS = "fa-laptop" },
                new Categoria { Id = 3, Nome = "Acessórios", IconCSS = "fa-headphones-alt" },
                new Categoria { Id = 4, Nome = "Videogames", IconCSS = "fa-gamepad" }
                );

            modelBuilder.Entity<Produto>().HasData(
              new Produto
              {
                  Id = 1,
                  Nome = "Samsung Galaxy S20",
                  Descricao = "Celular Samsung Galaxy S20",
                  ImagemUrl = "https://i.zst.com.br/thumbs/12/13/3e/-733839784.jpg",
                  Preco = 599.99m,
                  Quantidade = 10,
                  CategoriaId = 1
              },
              new Produto
              {
                  Id = 2,
                  Nome = "Iphone 15",
                  Descricao = "Celular Iphone 15",
                  ImagemUrl = "https://images.kabum.com.br/produtos/fotos/486277/iphone-15-apple-128gb-preto-tela-de-6-1-camera-dupla-de-48mp-ios-mtp03be-a_1695657254_original.jpg",
                  Preco = 1199.99m,
                  Quantidade = 110,
                  CategoriaId = 1
              },
              new Produto
              {
                  Id = 3,
                  Nome = "Xiaomi Redmi Note 10",
                  Descricao = "Celular Xiaomi Redmi Note 10",
                  ImagemUrl = "https://oneclick.com.py/upload/produtos/be88c00976d071c8406046514fdb4412.jpg",
                  Preco = 359.85m,
                  Quantidade = 120,
                  CategoriaId = 1
              },
              new Produto
              {
                  Id = 4,
                  Nome = "Computador Gamer Intel Core I5",
                  Descricao = "Computador Gamer Fácil Intel Core I5 3a Geração, 8GB, GTX 1650 4GB, SSD 120GB, Fonte 500w",
                  ImagemUrl = "https://images.kabum.com.br/produtos/fotos/sync_mirakl/169050/Computador-Gamer-F-cil-Intel-Core-I5-3a-Gera-o-8GB-GTX-1650-4GB-SSD-120GB-Fonte-500w_1669061915_gg.jpg",
                  Preco = 2599.78m,
                  Quantidade = 20,
                  CategoriaId = 2
              },
              new Produto
              {
                  Id = 5,
                  Nome = "Computador Gamer Intel Core I7",
                  Descricao = "Computador Gamer Fácil Intel Core I7 3a Geração, 8GB, GTX 1650 4GB, SSD 120GB, Fonte 500w",
                  ImagemUrl = "https://images.kabum.com.br/produtos/fotos/sync_mirakl/418694/PC-Gamer-Enifler-Completo-Intel-Core-I7-16GB-GTX-1050TI-4GB-SSD-480GB-Windows-10-Monitor-21-5-Kit-Gamer-Teclado-Mouse-E-Headset-Hayom_1685449128_g.jpg",
                  Preco = 3599.36m,
                  Quantidade = 20,
                  CategoriaId = 2
              },
              new Produto
              {
                  Id = 6,
                  Nome = "HUB USB 2.0 Bright, 4 Portas, Preto",
                  Descricao = "A linha de produtos Bright conta com qualidade e design para facilitar seu dia a dia.\r\n\r\nO Hub 4 portas Bright possui tamanho compacto, compatível com USB 2.0 ",
                  ImagemUrl = "https://images.kabum.com.br/produtos/fotos/104620/-mini-hub-usb-bright-4-portas-2-0-preto-59-_1569853689_gg.jpg",
                  Preco = 19.41m,
                  Quantidade = 20,
                  CategoriaId = 3
              },
              new Produto
              {
                  Id = 7,
                  Nome = "Headset Gamer Redragon Lamia, RGB, 7.1, Preto",
                  Descricao = "O Headset Gamer Redragon Lamia possui um design único, com iluminação RGB Chroma inteligente, que proporciona uma experiência visual incrível.",
                  ImagemUrl = "https://images.kabum.com.br/produtos/fotos/115413/headset-gamer-redragon-lamia-rgb-drivers-40mm-h320rgb-1-_1595016849_gg.jpg",
                  Preco = 199.53m,
                  Quantidade = 220,
                  CategoriaId = 3
              },
               new Produto
               {
                   Id = 8,
                   Nome = "Xbox Series S",
                   Descricao = "Console Microsoft Xbox Series S, 512GB, Branco. DESEMPENHO DE ÚLTIMA GERAÇÃO NO MENOR XBOX DE TODOS\r\nOS TEMPOS",
                   ImagemUrl = "https://images.kabum.com.br/produtos/fotos/sync_mirakl/200089/Console-Microsoft-Xbox-Series-S-512GB-Branco-RRS-00006_1699618095_gg.jpg",
                   Preco = 2587.82m,
                   Quantidade = 14,
                   CategoriaId = 4
               },
               new Produto
               {
                   Id = 9,
                   Nome = "Playstation 5",
                   Descricao = "Console Playstation 5, 825GB, Branco",
                   ImagemUrl = "https://images.kabum.com.br/produtos/fotos/238671/console-sony-playstation-5_1634132554_gg.jpg",
                   Preco = 3440.91m,
                   Quantidade = 206,
                   CategoriaId = 4
               }
             );

            modelBuilder.Entity<Usuario>()
                .HasData(new Usuario { Id = 1, Nome = "Cesar" });
            
            modelBuilder.Entity<Usuario>()
                .HasData(new Usuario { Id = 2, Nome = "Elba" });



            base.OnModelCreating(modelBuilder);
        }
    }
}
