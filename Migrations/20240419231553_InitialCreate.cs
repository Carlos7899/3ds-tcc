using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BairroConnectAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_CATEGORIA",
                columns: table => new
                {
                    idCategoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nomeCategoria = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CATEGORIA", x => x.idCategoria);
                });

            migrationBuilder.CreateTable(
                name: "TB_EVENTOMUNICIPE",
                columns: table => new
                {
                    idEventoMunicipe = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idMunicipe = table.Column<int>(type: "int", nullable: false),
                    idEvento = table.Column<int>(type: "int", nullable: false),
                    horaInicio = table.Column<DateTime>(type: "datetime", nullable: false),
                    horaFim = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_EVENTOMUNICIPE", x => x.idEventoMunicipe);
                });

            migrationBuilder.CreateTable(
                name: "TB_LOGINS",
                columns: table => new
                {
                    idPessoa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sobrenome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    dataNasc = table.Column<DateTime>(type: "date", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(255)", maxLength: 255, nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(255)", maxLength: 255, nullable: false),
                    Foto = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    tipoConta = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "Municipe")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_LOGINS", x => x.idPessoa);
                });

            migrationBuilder.CreateTable(
                name: "TB_MUNICIPE",
                columns: table => new
                {
                    idMunicipe = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idPessoa = table.Column<int>(type: "int", nullable: false),
                    estado = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    cidade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_MUNICIPE", x => x.idMunicipe);
                    table.ForeignKey(
                        name: "FK_TB_MUNICIPE_TB_LOGINS_idPessoa",
                        column: x => x.idPessoa,
                        principalTable: "TB_LOGINS",
                        principalColumn: "idPessoa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_ORGEVENTOS",
                columns: table => new
                {
                    idOrganizador = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idPessoa = table.Column<int>(type: "int", nullable: false),
                    profissao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    empresa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    telOrganizador = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ORGEVENTOS", x => x.idOrganizador);
                    table.ForeignKey(
                        name: "FK_TB_ORGEVENTOS_TB_LOGINS_idPessoa",
                        column: x => x.idPessoa,
                        principalTable: "TB_LOGINS",
                        principalColumn: "idPessoa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_EVENTO",
                columns: table => new
                {
                    idEvento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idOrganizador = table.Column<int>(type: "int", nullable: false),
                    idCategoria = table.Column<int>(type: "int", nullable: false),
                    titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dataFim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    limiteParticipantes = table.Column<int>(type: "int", nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    valorIngresso = table.Column<int>(type: "int", nullable: false),
                    horaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    horaFim = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_EVENTO", x => x.idEvento);
                    table.ForeignKey(
                        name: "FK_TB_EVENTO_TB_CATEGORIA_idCategoria",
                        column: x => x.idCategoria,
                        principalTable: "TB_CATEGORIA",
                        principalColumn: "idCategoria",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_EVENTO_TB_ORGEVENTOS_idOrganizador",
                        column: x => x.idOrganizador,
                        principalTable: "TB_ORGEVENTOS",
                        principalColumn: "idOrganizador",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_EQUIPE",
                columns: table => new
                {
                    idEvento = table.Column<int>(type: "int", nullable: false),
                    respoEquipe = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    tamanhoEquipe = table.Column<int>(type: "int", nullable: false),
                    SelecaoEquipe = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_TB_EQUIPE_TB_EVENTO_idEvento",
                        column: x => x.idEvento,
                        principalTable: "TB_EVENTO",
                        principalColumn: "idEvento",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_EVENTOCOMENTARIO",
                columns: table => new
                {
                    idEvento = table.Column<int>(type: "int", nullable: false),
                    idMunicipe = table.Column<int>(type: "int", nullable: false),
                    comentario = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    avaliacao = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_TB_EVENTOCOMENTARIO_TB_EVENTO_idEvento",
                        column: x => x.idEvento,
                        principalTable: "TB_EVENTO",
                        principalColumn: "idEvento",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_EVENTOENDERECO",
                columns: table => new
                {
                    idEvento = table.Column<int>(type: "int", nullable: false),
                    endereco = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    nroEndereco = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Complemento = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    bairroEndereco = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    cidadeEndereco = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UFEndereco = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    CEPEndereco = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_TB_EVENTOENDERECO_TB_EVENTO_idEvento",
                        column: x => x.idEvento,
                        principalTable: "TB_EVENTO",
                        principalColumn: "idEvento",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_EVENTOPARTICIPANTE",
                columns: table => new
                {
                    idEvento = table.Column<int>(type: "int", nullable: false),
                    horaParticipacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    limiteParticipantesHora = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_TB_EVENTOPARTICIPANTE_TB_EVENTO_idEvento",
                        column: x => x.idEvento,
                        principalTable: "TB_EVENTO",
                        principalColumn: "idEvento",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TB_CATEGORIA",
                columns: new[] { "idCategoria", "descricao", "nomeCategoria" },
                values: new object[,]
                {
                    { 1, "Atividades físicas e competições recreativas.", "Esportivo" },
                    { 2, "Diversão e lazer para todos os gostos.", "Entreterimento" },
                    { 3, "Exploração da arte, história e tradições.", "Cultaral" },
                    { 4, "Eventos voltados para negócios.", "Corporativo" },
                    { 5, "Práticas e celebrações voltadas para a religião.", "Religioso" },
                    { 7, "Eventos voltados para educação", "Educacional" },
                    { 8, "Eventos relacionados a organizações e instituições.", "Institucional" }
                });

            migrationBuilder.InsertData(
                table: "TB_LOGINS",
                columns: new[] { "idPessoa", "Foto", "PasswordHash", "PasswordSalt", "dataNasc", "email", "nome", "sobrenome", "tipoConta" },
                values: new object[] { 1, null, new byte[] { 214, 26, 66, 10, 128, 175, 6, 39, 163, 37, 129, 230, 36, 160, 136, 230, 18, 155, 109, 90, 0, 216, 239, 40, 23, 8, 168, 216, 205, 159, 149, 230, 226, 235, 220, 114, 0, 137, 44, 2, 27, 242, 244, 142, 114, 147, 38, 178, 103, 91, 196, 76, 14, 15, 146, 208, 89, 212, 3, 157, 115, 168, 173, 80 }, new byte[] { 148, 100, 163, 91, 57, 107, 179, 59, 220, 18, 142, 136, 61, 3, 48, 149, 22, 153, 117, 164, 134, 202, 79, 222, 159, 240, 245, 123, 127, 34, 8, 87, 23, 27, 237, 73, 1, 194, 152, 104, 227, 141, 122, 37, 232, 251, 227, 122, 94, 142, 12, 63, 126, 75, 132, 25, 140, 208, 116, 255, 236, 189, 230, 239, 248, 176, 17, 226, 233, 231, 35, 95, 29, 83, 39, 244, 208, 117, 224, 212, 103, 13, 173, 23, 237, 112, 106, 12, 99, 188, 151, 52, 185, 217, 249, 241, 52, 195, 185, 60, 27, 53, 22, 253, 163, 215, 121, 131, 103, 138, 79, 34, 93, 25, 187, 32, 88, 203, 165, 8, 43, 254, 190, 37, 91, 112, 107, 115 }, new DateTime(2024, 4, 19, 20, 15, 52, 520, DateTimeKind.Local).AddTicks(1945), "seuEmail@gmail.com", "UsuarioAdmin", "", "Organizador" });

            migrationBuilder.CreateIndex(
                name: "IX_TB_EQUIPE_idEvento",
                table: "TB_EQUIPE",
                column: "idEvento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_EVENTO_idCategoria",
                table: "TB_EVENTO",
                column: "idCategoria",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_EVENTO_idOrganizador",
                table: "TB_EVENTO",
                column: "idOrganizador",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_EVENTOCOMENTARIO_idEvento",
                table: "TB_EVENTOCOMENTARIO",
                column: "idEvento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_EVENTOENDERECO_idEvento",
                table: "TB_EVENTOENDERECO",
                column: "idEvento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_EVENTOPARTICIPANTE_idEvento",
                table: "TB_EVENTOPARTICIPANTE",
                column: "idEvento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_MUNICIPE_idPessoa",
                table: "TB_MUNICIPE",
                column: "idPessoa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_ORGEVENTOS_idPessoa",
                table: "TB_ORGEVENTOS",
                column: "idPessoa",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_EQUIPE");

            migrationBuilder.DropTable(
                name: "TB_EVENTOCOMENTARIO");

            migrationBuilder.DropTable(
                name: "TB_EVENTOENDERECO");

            migrationBuilder.DropTable(
                name: "TB_EVENTOMUNICIPE");

            migrationBuilder.DropTable(
                name: "TB_EVENTOPARTICIPANTE");

            migrationBuilder.DropTable(
                name: "TB_MUNICIPE");

            migrationBuilder.DropTable(
                name: "TB_EVENTO");

            migrationBuilder.DropTable(
                name: "TB_CATEGORIA");

            migrationBuilder.DropTable(
                name: "TB_ORGEVENTOS");

            migrationBuilder.DropTable(
                name: "TB_LOGINS");
        }
    }
}
