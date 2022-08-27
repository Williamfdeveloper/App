using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Repository.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_FORMA_PAGAMENTO",
                schema: "dbo",
                columns: table => new
                {
                    CD_FORMA_PAGAMENTO = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DC_FORMA_PAGAMENTO = table.Column<string>(nullable: false),
                    ID_TIPO_FORMA_PAGAMENTO = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_FORMA_PAGAMENTO", x => x.CD_FORMA_PAGAMENTO);
                });

            migrationBuilder.CreateTable(
                name: "TB_PEDIDO_PAGAMENTO",
                schema: "dbo",
                columns: table => new
                {
                    CD_PEDIDO_PAGAMENTO = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CD_PEDIDO = table.Column<int>(nullable: false),
                    ID_SITUACAO_PAGAMENTO = table.Column<int>(nullable: false),
                    DH_PAGAMENTO = table.Column<DateTime>(nullable: false),
                    DH_PAGAMENTO_APROVADO = table.Column<DateTime>(nullable: false),
                    DH_ATUALIZACAO = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PEDIDO_PAGAMENTO", x => x.CD_PEDIDO_PAGAMENTO);
                });

            migrationBuilder.CreateTable(
                name: "TB_PEDIDO_PAGAMENTO_HISTORICO",
                schema: "dbo",
                columns: table => new
                {
                    CD_PEDIDO_PAGAMENTO_HISTORICO = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CD_PEDIDO_PAGAMENTO = table.Column<int>(nullable: false),
                    ID_SITUACAO_PAGAMENTO = table.Column<int>(nullable: false),
                    DH_ATUALIZACAO = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PEDIDO_PAGAMENTO_HISTORICO", x => x.CD_PEDIDO_PAGAMENTO_HISTORICO);
                });

            migrationBuilder.CreateTable(
                name: "TB_PESSOA_CARTAO",
                schema: "dbo",
                columns: table => new
                {
                    CD_CARTAO = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CD_USUARIO = table.Column<Guid>(nullable: false),
                    DC_NUMERO_CARTAO = table.Column<string>(nullable: false),
                    DC_HASH_CARTAO = table.Column<string>(nullable: false),
                    DT_VENCIMENTO = table.Column<DateTime>(nullable: false),
                    DC_SENHA = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PESSOA_CARTAO", x => x.CD_CARTAO);
                });

            migrationBuilder.CreateTable(
                name: "TB_USUARIO",
                schema: "dbo",
                columns: table => new
                {
                    CD_DADO_USUARIO = table.Column<Guid>(nullable: false),
                    DC_NOME = table.Column<string>(nullable: false),
                    DC_CPF = table.Column<string>(nullable: false),
                    DT_NASCIMENTO = table.Column<DateTime>(nullable: false),
                    ID_SEXO = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USUARIO", x => x.CD_DADO_USUARIO);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_PEDIDO",
                schema: "dbo",
                columns: table => new
                {
                    CD_PEDIDO = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CD_USUARIO = table.Column<Guid>(nullable: false),
                    MO_VALOR_TOTAL = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MO_VALOR_TOTAL_COM_DESCONTO = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QT_ITENS_VENDA = table.Column<int>(nullable: false),
                    DH_PEDIDO = table.Column<DateTime>(nullable: false),
                    DH_APROVACAO_PEDIDO = table.Column<DateTime>(nullable: false),
                    ID_SITUACAO_PEDIDO = table.Column<int>(nullable: false),
                    usuarioId = table.Column<string>(nullable: true),
                    PedidoPagamentoCodigoPedidoPagamento = table.Column<int>(nullable: true),
                    UsuarioCodigo = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PEDIDO", x => x.CD_PEDIDO);
                    table.ForeignKey(
                        name: "FK_TB_PEDIDO_TB_PEDIDO_PAGAMENTO_PedidoPagamentoCodigoPedidoPagamento",
                        column: x => x.PedidoPagamentoCodigoPedidoPagamento,
                        principalSchema: "dbo",
                        principalTable: "TB_PEDIDO_PAGAMENTO",
                        principalColumn: "CD_PEDIDO_PAGAMENTO",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_PEDIDO_TB_USUARIO_UsuarioCodigo",
                        column: x => x.UsuarioCodigo,
                        principalSchema: "dbo",
                        principalTable: "TB_USUARIO",
                        principalColumn: "CD_DADO_USUARIO",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_PEDIDO_AspNetUsers_usuarioId",
                        column: x => x.usuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_PESSOA_ENDERECO",
                schema: "dbo",
                columns: table => new
                {
                    ID_ENDERECO = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CD_USUARIO = table.Column<Guid>(nullable: false),
                    DC_RUA = table.Column<string>(nullable: false),
                    DC_NUMERO = table.Column<string>(nullable: false),
                    DC_BAIRRO = table.Column<string>(nullable: false),
                    DC_CIDADE = table.Column<string>(nullable: false),
                    DC_ESTADO = table.Column<string>(nullable: false),
                    DC_CEP = table.Column<string>(nullable: false),
                    UsuarioCodigo = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PESSOA_ENDERECO", x => x.ID_ENDERECO);
                    table.ForeignKey(
                        name: "FK_TB_PESSOA_ENDERECO_TB_USUARIO_UsuarioCodigo",
                        column: x => x.UsuarioCodigo,
                        principalSchema: "dbo",
                        principalTable: "TB_USUARIO",
                        principalColumn: "CD_DADO_USUARIO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_PEDIDO_HISTORICO",
                schema: "dbo",
                columns: table => new
                {
                    CD_PEDIDO = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_SITUACAO = table.Column<int>(nullable: false),
                    DH_SITUACAO = table.Column<DateTime>(nullable: false),
                    DH_SITUACAO_INICIO = table.Column<DateTime>(nullable: false),
                    DH_SITUACAO_FIM = table.Column<DateTime>(nullable: false),
                    PedidoCodigoPedido = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PEDIDO_HISTORICO", x => x.CD_PEDIDO);
                    table.ForeignKey(
                        name: "FK_TB_PEDIDO_HISTORICO_TB_PEDIDO_PedidoCodigoPedido",
                        column: x => x.PedidoCodigoPedido,
                        principalSchema: "dbo",
                        principalTable: "TB_PEDIDO",
                        principalColumn: "CD_PEDIDO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_PEDIDO_ITEM",
                schema: "dbo",
                columns: table => new
                {
                    CD_PEDIDO_ITEM = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CD_PEDIDO = table.Column<int>(nullable: false),
                    CD_PRODUTOR = table.Column<int>(nullable: false),
                    QT_ITEM = table.Column<int>(maxLength: 2, nullable: false),
                    MO_VALOR_UNITARIO = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MO_VALOR_TOTAL = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PedidoCodigoPedido = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PEDIDO_ITEM", x => x.CD_PEDIDO_ITEM);
                    table.ForeignKey(
                        name: "FK_TB_PEDIDO_ITEM_TB_PEDIDO_PedidoCodigoPedido",
                        column: x => x.PedidoCodigoPedido,
                        principalSchema: "dbo",
                        principalTable: "TB_PEDIDO",
                        principalColumn: "CD_PEDIDO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_PRODUTO",
                schema: "dbo",
                columns: table => new
                {
                    CD_PRODUTO = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DC_DESCRICAO = table.Column<string>(nullable: false),
                    MO_VALOR = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DC_LINK = table.Column<string>(nullable: false),
                    PedidoItemCodigoPedidoItem = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PRODUTO", x => x.CD_PRODUTO);
                    table.ForeignKey(
                        name: "FK_TB_PRODUTO_TB_PEDIDO_ITEM_PedidoItemCodigoPedidoItem",
                        column: x => x.PedidoItemCodigoPedidoItem,
                        principalSchema: "dbo",
                        principalTable: "TB_PEDIDO_ITEM",
                        principalColumn: "CD_PEDIDO_ITEM",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "TB_FORMA_PAGAMENTO",
                columns: new[] { "CD_FORMA_PAGAMENTO", "DC_FORMA_PAGAMENTO", "ID_TIPO_FORMA_PAGAMENTO" },
                values: new object[,]
                {
                    { 1, "Boleto", 1 },
                    { 2, "Credito", 2 },
                    { 3, "Debito", 3 }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "TB_PRODUTO",
                columns: new[] { "CD_PRODUTO", "DC_DESCRICAO", "DC_LINK", "PedidoItemCodigoPedidoItem", "MO_VALOR" },
                values: new object[,]
                {
                    { 1, "Aplicativo Teste 001", "http://localhost", null, 500m },
                    { 2, "Aplicativo Teste 002", "http://localhost", null, 100m },
                    { 3, "Aplicativo Teste 003", "http://localhost", null, 300m },
                    { 4, "Aplicativo Teste 004", "http://localhost", null, 800m },
                    { 5, "Aplicativo Teste 005", "http://localhost", null, 500m },
                    { 6, "Aplicativo Teste 006", "http://localhost", null, 500m },
                    { 7, "Aplicativo Teste 007", "http://localhost", null, 500m },
                    { 8, "Aplicativo Teste 008", "http://localhost", null, 500m },
                    { 9, "Aplicativo Teste 009", "http://localhost", null, 500m },
                    { 10, "Aplicativo Teste 010", "http://localhost", null, 500m },
                    { 11, "Aplicativo Teste 011", "http://localhost", null, 500m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PEDIDO_PedidoPagamentoCodigoPedidoPagamento",
                schema: "dbo",
                table: "TB_PEDIDO",
                column: "PedidoPagamentoCodigoPedidoPagamento");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PEDIDO_UsuarioCodigo",
                schema: "dbo",
                table: "TB_PEDIDO",
                column: "UsuarioCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PEDIDO_usuarioId",
                schema: "dbo",
                table: "TB_PEDIDO",
                column: "usuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PEDIDO_HISTORICO_PedidoCodigoPedido",
                schema: "dbo",
                table: "TB_PEDIDO_HISTORICO",
                column: "PedidoCodigoPedido");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PEDIDO_ITEM_PedidoCodigoPedido",
                schema: "dbo",
                table: "TB_PEDIDO_ITEM",
                column: "PedidoCodigoPedido");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PESSOA_ENDERECO_UsuarioCodigo",
                schema: "dbo",
                table: "TB_PESSOA_ENDERECO",
                column: "UsuarioCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PRODUTO_PedidoItemCodigoPedidoItem",
                schema: "dbo",
                table: "TB_PRODUTO",
                column: "PedidoItemCodigoPedidoItem");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "TB_FORMA_PAGAMENTO",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TB_PEDIDO_HISTORICO",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TB_PEDIDO_PAGAMENTO_HISTORICO",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TB_PESSOA_CARTAO",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TB_PESSOA_ENDERECO",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TB_PRODUTO",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "TB_PEDIDO_ITEM",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TB_PEDIDO",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TB_PEDIDO_PAGAMENTO",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TB_USUARIO",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
