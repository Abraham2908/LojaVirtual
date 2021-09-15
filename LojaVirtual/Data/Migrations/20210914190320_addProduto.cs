using Microsoft.EntityFrameworkCore.Migrations;

namespace LojaVirtual.Data.Migrations
{
    public partial class addProduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false),
                    Preco = table.Column<decimal>(nullable: false),
                    Imagem = table.Column<string>(nullable: true),
                    ProdutoCor = table.Column<string>(nullable: true),
                    Disponibilidade = table.Column<bool>(nullable: false),
                    CategoriaId = table.Column<int>(nullable: false),
                    TagEspecialId = table.Column<int>(nullable: false)
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
                    table.ForeignKey(
                        name: "FK_Produtos_TagEspecial_TagEspecialId",
                        column: x => x.TagEspecialId,
                        principalTable: "TagEspecial",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CategoriaId",
                table: "Produtos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_TagEspecialId",
                table: "Produtos",
                column: "TagEspecialId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produtos");
        }
    }
}
