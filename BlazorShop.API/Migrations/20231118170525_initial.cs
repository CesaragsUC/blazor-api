using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlazorShop.API.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IconCSS = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ImagemUrl = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produtos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carrinhos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrinhos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carrinhos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarrinhoItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarrinhoId = table.Column<int>(type: "int", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrinhoItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarrinhoItems_Carrinhos_CarrinhoId",
                        column: x => x.CarrinhoId,
                        principalTable: "Carrinhos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarrinhoItems_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "IconCSS", "Nome" },
                values: new object[,]
                {
                    { 1, "fa-mobile-alt", "Celulares" },
                    { 2, "fa-laptop", "Computadores" },
                    { 3, "fa-headphones-alt", "Acessórios" },
                    { 4, "fa-gamepad", "Videogames" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Cesar" },
                    { 2, "Elba" }
                });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "CategoriaId", "Descricao", "ImagemUrl", "Nome", "Preco", "Quantidade" },
                values: new object[,]
                {
                    { 1, 1, "Celular Samsung Galaxy S20", "https://i.zst.com.br/thumbs/12/13/3e/-733839784.jpg", "Samsung Galaxy S20", 599.99m, 10 },
                    { 2, 1, "Celular Iphone 15", "https://images.kabum.com.br/produtos/fotos/486277/iphone-15-apple-128gb-preto-tela-de-6-1-camera-dupla-de-48mp-ios-mtp03be-a_1695657254_original.jpg", "Iphone 15", 1199.99m, 110 },
                    { 3, 1, "Celular Xiaomi Redmi Note 10", "https://oneclick.com.py/upload/produtos/be88c00976d071c8406046514fdb4412.jpg", "Xiaomi Redmi Note 10", 359.85m, 120 },
                    { 4, 2, "Computador Gamer Fácil Intel Core I5 3a Geração, 8GB, GTX 1650 4GB, SSD 120GB, Fonte 500w", "https://images.kabum.com.br/produtos/fotos/sync_mirakl/169050/Computador-Gamer-F-cil-Intel-Core-I5-3a-Gera-o-8GB-GTX-1650-4GB-SSD-120GB-Fonte-500w_1669061915_gg.jpg", "Computador Gamer Intel Core I5", 2599.78m, 20 },
                    { 5, 2, "Computador Gamer Fácil Intel Core I7 3a Geração, 8GB, GTX 1650 4GB, SSD 120GB, Fonte 500w", "https://images.kabum.com.br/produtos/fotos/sync_mirakl/418694/PC-Gamer-Enifler-Completo-Intel-Core-I7-16GB-GTX-1050TI-4GB-SSD-480GB-Windows-10-Monitor-21-5-Kit-Gamer-Teclado-Mouse-E-Headset-Hayom_1685449128_g.jpg", "Computador Gamer Intel Core I7", 3599.36m, 20 },
                    { 6, 3, "A linha de produtos Bright conta com qualidade e design para facilitar seu dia a dia.\r\n\r\nO Hub 4 portas Bright possui tamanho compacto, compatível com USB 2.0 ", "https://images.kabum.com.br/produtos/fotos/104620/-mini-hub-usb-bright-4-portas-2-0-preto-59-_1569853689_gg.jpg", "HUB USB 2.0 Bright, 4 Portas, Preto", 19.41m, 20 },
                    { 7, 3, "O Headset Gamer Redragon Lamia possui um design único, com iluminação RGB Chroma inteligente, que proporciona uma experiência visual incrível.", "https://images.kabum.com.br/produtos/fotos/115413/headset-gamer-redragon-lamia-rgb-drivers-40mm-h320rgb-1-_1595016849_gg.jpg", "Headset Gamer Redragon Lamia, RGB, 7.1, Preto", 199.53m, 220 },
                    { 8, 4, "Console Microsoft Xbox Series S, 512GB, Branco. DESEMPENHO DE ÚLTIMA GERAÇÃO NO MENOR XBOX DE TODOS\r\nOS TEMPOS", "https://images.kabum.com.br/produtos/fotos/sync_mirakl/200089/Console-Microsoft-Xbox-Series-S-512GB-Branco-RRS-00006_1699618095_gg.jpg", "Xbox Series S", 2587.82m, 14 },
                    { 9, 4, "Console Playstation 5, 825GB, Branco", "https://images.kabum.com.br/produtos/fotos/238671/console-sony-playstation-5_1634132554_gg.jpg", "Playstation 5", 3440.91m, 206 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoItems_CarrinhoId",
                table: "CarrinhoItems",
                column: "CarrinhoId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoItems_ProdutoId",
                table: "CarrinhoItems",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Carrinhos_UsuarioId",
                table: "Carrinhos",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CategoriaId",
                table: "Produtos",
                column: "CategoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarrinhoItems");

            migrationBuilder.DropTable(
                name: "Carrinhos");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
