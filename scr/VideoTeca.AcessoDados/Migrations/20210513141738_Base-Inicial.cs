using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VideoTeca.AcessoDados.Migrations
{
    public partial class BaseInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "catalogo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cod_catalogo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    id_genero = table.Column<int>(type: "int", nullable: false),
                    des_titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nom_autor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ano_lancamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dtc_atualizacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catalogo", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    num_documento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cod_tipo_pessoa = table.Column<byte>(type: "tinyint", nullable: false),
                    nom_cliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    num_telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    des_logradouro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    des_complemento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    num_endereco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    num_cep = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    des_bairro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    des_municipio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dtc_nascimento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dtc_atualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Genero",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    des_genero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dtc_atualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genero", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tipo_midia",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    des_tipo_midia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dtc_atualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipo_midia", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "catalogo_tipo_midia",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_catalogo = table.Column<int>(type: "int", nullable: false),
                    id_tipo_midia = table.Column<int>(type: "int", nullable: false),
                    qtd_titulo = table.Column<int>(type: "int", nullable: false),
                    qtd_disponivel = table.Column<int>(type: "int", nullable: false),
                    dtc_atualizacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catalogo_tipo_midia", x => x.id);
                    table.ForeignKey(
                        name: "FK_catalogo_tipo_midia_catalogo_id_catalogo",
                        column: x => x.id_catalogo,
                        principalTable: "catalogo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catalogo_tipo_midia_tipo_midia_id_tipo_midia",
                        column: x => x.id_tipo_midia,
                        principalTable: "tipo_midia",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cliente_catalogo_tipo_midia",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_cliente = table.Column<int>(type: "int", nullable: false),
                    id_catalogo_tipo_midia = table.Column<int>(type: "int", nullable: false),
                    qtd_titulo = table.Column<int>(type: "int", nullable: false),
                    dtc_locacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtc_entrega = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtc_devolucao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dtc_atualizacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cliente_catalogo_tipo_midia", x => x.id);
                    table.ForeignKey(
                        name: "FK_cliente_catalogo_tipo_midia_catalogo_tipo_midia_id_catalogo_tipo_midia",
                        column: x => x.id_catalogo_tipo_midia,
                        principalTable: "catalogo_tipo_midia",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cliente_catalogo_tipo_midia_Cliente_id_cliente",
                        column: x => x.id_cliente,
                        principalTable: "Cliente",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_catalogo_tipo_midia_id_catalogo",
                table: "catalogo_tipo_midia",
                column: "id_catalogo");

            migrationBuilder.CreateIndex(
                name: "IX_catalogo_tipo_midia_id_tipo_midia",
                table: "catalogo_tipo_midia",
                column: "id_tipo_midia");

            migrationBuilder.CreateIndex(
                name: "IX_cliente_catalogo_tipo_midia_id_catalogo_tipo_midia",
                table: "cliente_catalogo_tipo_midia",
                column: "id_catalogo_tipo_midia");

            migrationBuilder.CreateIndex(
                name: "IX_cliente_catalogo_tipo_midia_id_cliente",
                table: "cliente_catalogo_tipo_midia",
                column: "id_cliente");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cliente_catalogo_tipo_midia");

            migrationBuilder.DropTable(
                name: "Genero");

            migrationBuilder.DropTable(
                name: "catalogo_tipo_midia");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "catalogo");

            migrationBuilder.DropTable(
                name: "tipo_midia");
        }
    }
}
