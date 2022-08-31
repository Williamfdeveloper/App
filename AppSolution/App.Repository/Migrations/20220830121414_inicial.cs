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
                name: "TB_EMAIL_SISTEMA",
                schema: "dbo",
                columns: table => new
                {
                    GUID_EMAIL = table.Column<Guid>(nullable: false),
                    DC_SERVIDOR_SMTP = table.Column<string>(nullable: false),
                    DC_PORT_SMTP = table.Column<int>(nullable: false),
                    DC_USERNAME = table.Column<string>(nullable: false),
                    DC_USER_PASSWORD = table.Column<string>(nullable: false),
                    ServerNameDisplay = table.Column<string>(nullable: true),
                    ID_TIPO_EMAIL = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_EMAIL_SISTEMA", x => x.GUID_EMAIL);
                });

            migrationBuilder.CreateTable(
                name: "TB_FORMA_PAGAMENTO",
                schema: "dbo",
                columns: table => new
                {
                    CD_FORMA_PAGAMENTO = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DC_FORMA_PAGAMENTO = table.Column<string>(nullable: false),
                    QuantidadeParcelas = table.Column<int>(nullable: false),
                    ValorDesconto = table.Column<decimal>(nullable: false),
                    TipoDesconto = table.Column<int>(nullable: false),
                    ID_TIPO_FORMA_PAGAMENTO = table.Column<int>(nullable: false),
                    Ativa = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_FORMA_PAGAMENTO", x => x.CD_FORMA_PAGAMENTO);
                });

            migrationBuilder.CreateTable(
                name: "TB_LOG_ERRO",
                schema: "dbo",
                columns: table => new
                {
                    CD_LOG_ERRO = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CD_ERRO = table.Column<int>(nullable: false),
                    DC_MESSAGE = table.Column<string>(nullable: false),
                    DC_EXCEPTION = table.Column<string>(nullable: false),
                    DH_LOG_ERRO = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_LOG_ERRO", x => x.CD_LOG_ERRO);
                });

            migrationBuilder.CreateTable(
                name: "TB_PARAMETROS_SISTEMA",
                schema: "dbo",
                columns: table => new
                {
                    CD_PARAMETRO_SISTEMA = table.Column<Guid>(nullable: false),
                    DC_NAME = table.Column<string>(nullable: false),
                    DC_VALOR = table.Column<string>(nullable: false),
                    DC_DESCRICAO = table.Column<string>(nullable: false),
                    ID_TIPO_CAMPO = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PARAMETROS_SISTEMA", x => x.CD_PARAMETRO_SISTEMA);
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
                    table.PrimaryKey("PK_TB_PEDIDO_PAGAMENTO_HISTORICO", x => new { x.CD_PEDIDO_PAGAMENTO_HISTORICO, x.CD_PEDIDO_PAGAMENTO });
                });

            migrationBuilder.CreateTable(
                name: "TB_PESSOA_CARTAO",
                schema: "dbo",
                columns: table => new
                {
                    CD_CARTAO = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CD_USUARIO = table.Column<string>(nullable: false),
                    DC_NUMERO_CARTAO = table.Column<string>(nullable: false),
                    DC_NOME_CARTAO = table.Column<string>(nullable: false),
                    DC_HASH_CARTAO = table.Column<string>(nullable: false),
                    DT_VENCIMENTO = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PESSOA_CARTAO", x => x.CD_CARTAO);
                });

            migrationBuilder.CreateTable(
                name: "TB_PESSOA_ENDERECO",
                schema: "dbo",
                columns: table => new
                {
                    ID_ENDERECO = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CD_USUARIO = table.Column<string>(nullable: false),
                    DC_RUA = table.Column<string>(nullable: false),
                    DC_NUMERO = table.Column<string>(nullable: false),
                    DC_BAIRRO = table.Column<string>(nullable: false),
                    DC_CIDADE = table.Column<string>(nullable: false),
                    DC_ESTADO = table.Column<string>(nullable: false),
                    DC_CEP = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PESSOA_ENDERECO", x => x.ID_ENDERECO);
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
                    DC_LINK = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PRODUTO", x => x.CD_PRODUTO);
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
                    DH_ATUALIZACAO = table.Column<DateTime>(nullable: false),
                    PedidoPagamentoHistoricoCodigoPedidoPagamentoHistorico = table.Column<int>(nullable: true),
                    PedidoPagamentoHistoricoCodigoPedidoPagamento = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PEDIDO_PAGAMENTO", x => x.CD_PEDIDO_PAGAMENTO);
                    table.ForeignKey(
                        name: "FK_TB_PEDIDO_PAGAMENTO_TB_PEDIDO_PAGAMENTO_HISTORICO_PedidoPagamentoHistoricoCodigoPedidoPagamentoHistorico_PedidoPagamentoHist~",
                        columns: x => new { x.PedidoPagamentoHistoricoCodigoPedidoPagamentoHistorico, x.PedidoPagamentoHistoricoCodigoPedidoPagamento },
                        principalSchema: "dbo",
                        principalTable: "TB_PEDIDO_PAGAMENTO_HISTORICO",
                        principalColumns: new[] { "CD_PEDIDO_PAGAMENTO_HISTORICO", "CD_PEDIDO_PAGAMENTO" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "dbo",
                columns: table => new
                {
                    CD_USUARIO = table.Column<string>(nullable: false),
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
                    AccessFailedCount = table.Column<int>(nullable: false),
                    DC_NOME = table.Column<string>(nullable: false),
                    DC_CPF = table.Column<string>(nullable: false),
                    DT_NASCIMENTO = table.Column<DateTime>(nullable: false),
                    ID_SEXO = table.Column<int>(nullable: false),
                    EnderecoCodigoEndereco = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.CD_USUARIO);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_TB_PESSOA_ENDERECO_EnderecoCodigoEndereco",
                        column: x => x.EnderecoCodigoEndereco,
                        principalSchema: "dbo",
                        principalTable: "TB_PESSOA_ENDERECO",
                        principalColumn: "ID_ENDERECO",
                        onDelete: ReferentialAction.Restrict);
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
                        principalSchema: "dbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "CD_USUARIO",
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
                        principalSchema: "dbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "CD_USUARIO",
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
                        principalSchema: "dbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "CD_USUARIO",
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
                        principalSchema: "dbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "CD_USUARIO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_PEDIDO",
                schema: "dbo",
                columns: table => new
                {
                    CD_PEDIDO = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CD_USUARIO = table.Column<string>(nullable: false),
                    MO_VALOR_TOTAL = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MO_VALOR_TOTAL_COM_DESCONTO = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QT_ITENS_VENDA = table.Column<int>(nullable: false),
                    DH_CAPTACAO_PEDIDO = table.Column<DateTime>(nullable: false),
                    DH_PEDIDO = table.Column<DateTime>(nullable: false),
                    DH_APROVACAO_PEDIDO = table.Column<DateTime>(nullable: false),
                    ID_SITUACAO_PEDIDO = table.Column<int>(nullable: false),
                    DC_CUPOM = table.Column<string>(nullable: false),
                    CD_FORMA_PAGAMENTO = table.Column<int>(nullable: false),
                    PedidoPagamentoCodigoPedidoPagamento = table.Column<int>(nullable: true),
                    UsuarioId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PEDIDO", x => x.CD_PEDIDO);
                    table.ForeignKey(
                        name: "FK_TB_PEDIDO_TB_FORMA_PAGAMENTO_CD_FORMA_PAGAMENTO",
                        column: x => x.CD_FORMA_PAGAMENTO,
                        principalSchema: "dbo",
                        principalTable: "TB_FORMA_PAGAMENTO",
                        principalColumn: "CD_FORMA_PAGAMENTO",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_PEDIDO_TB_PEDIDO_PAGAMENTO_PedidoPagamentoCodigoPedidoPagamento",
                        column: x => x.PedidoPagamentoCodigoPedidoPagamento,
                        principalSchema: "dbo",
                        principalTable: "TB_PEDIDO_PAGAMENTO",
                        principalColumn: "CD_PEDIDO_PAGAMENTO",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_PEDIDO_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "dbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "CD_USUARIO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_PEDIDO_HISTORICO",
                schema: "dbo",
                columns: table => new
                {
                    CD_PEDIDO_HISTORICO = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CD_PEDIDO = table.Column<int>(nullable: false),
                    ID_SITUACAO = table.Column<int>(nullable: false),
                    DH_SITUACAO = table.Column<DateTime>(nullable: false),
                    DH_SITUACAO_INICIO = table.Column<DateTime>(nullable: false),
                    DH_SITUACAO_FIM = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PEDIDO_HISTORICO", x => new { x.CD_PEDIDO, x.CD_PEDIDO_HISTORICO });
                    table.ForeignKey(
                        name: "FK_TB_PEDIDO_HISTORICO_TB_PEDIDO_CD_PEDIDO",
                        column: x => x.CD_PEDIDO,
                        principalSchema: "dbo",
                        principalTable: "TB_PEDIDO",
                        principalColumn: "CD_PEDIDO",
                        onDelete: ReferentialAction.Cascade);
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
                    DC_PRODUTO = table.Column<string>(nullable: false),
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

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "TB_EMAIL_SISTEMA",
                columns: new[] { "GUID_EMAIL", "DC_SERVIDOR_SMTP", "DC_PORT_SMTP", "ServerNameDisplay", "ID_TIPO_EMAIL", "DC_USERNAME", "DC_USER_PASSWORD" },
                values: new object[] { new Guid("c87d8cff-2adb-424a-a1bf-6e08812af526"), "mail.v2wstore.com.br", 465, "V2WMedia", 1, "williamf.developer@outlook.com", "BR@sil500" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "TB_FORMA_PAGAMENTO",
                columns: new[] { "CD_FORMA_PAGAMENTO", "Ativa", "DC_FORMA_PAGAMENTO", "ID_TIPO_FORMA_PAGAMENTO", "QuantidadeParcelas", "TipoDesconto", "ValorDesconto" },
                values: new object[,]
                {
                    { 1, false, "Boleto", 1, 0, 0, 0m },
                    { 2, false, "Credito", 2, 0, 0, 0m },
                    { 3, false, "Debito", 3, 0, 0, 0m }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "TB_PARAMETROS_SISTEMA",
                columns: new[] { "CD_PARAMETRO_SISTEMA", "DC_DESCRICAO", "DC_NAME", "ID_TIPO_CAMPO", "DC_VALOR" },
                values: new object[,]
                {
                    { new Guid("f4ac62b7-1f10-4825-9c21-b547ed9d8f11"), "Parametro para validar como será salvo as imagens 1 - Banco | 2 Arquivo", "ParametroTipoImagem", 1, "2" },
                    { new Guid("e5e29377-bf2b-42b3-b582-afdaa23d57c2"), "Parametro para validar se deve ser enviado Email de Confirmação de email", "ParametroEnviarConfirmacaoEmail", 1, "false" },
                    { new Guid("95f1d58f-f267-42a7-b997-66b019d97736"), "Parametro para validar se deve ser enviado Email de reset de senha", "ParametroEnviarEmailResetSenha", 1, "false" },
                    { new Guid("777b458b-95f0-4190-a247-9781bb5ca88d"), "Parametro utilizado para identificar qual a pasta que será salvo as imagens.", "ParametroCaminhoPastaArquivo", 1, "wwwroot\\imagens" },
                    { new Guid("d101d5ca-b0ba-4d35-a40e-40d5844721b5"), "Email para contato", "EmailContato", 1, "williamf.developer@gmail.com;willian.fer21@gmail.com" },
                    { new Guid("69bd532c-e906-4191-8d49-632d5b08ca14"), "Template para enviar email de reset de senha", "HtmlTemplateEmailDefault", 1, "PCFET0NUWVBFIGh0bWw+CjxodG1sPgoKPGhlYWQ+CiAgICA8dGl0bGU+PC90aXRsZT4KICAgIDxtZXRhIGh0dHAtZXF1aXY9IkNvbnRlbnQtVHlwZSIgY29udGVudD0idGV4dC9odG1sOyBjaGFyc2V0PXV0Zi04IiAvPgogICAgPG1ldGEgbmFtZT0idmlld3BvcnQiIGNvbnRlbnQ9IndpZHRoPWRldmljZS13aWR0aCwgaW5pdGlhbC1zY2FsZT0xIj4KICAgIDxtZXRhIGh0dHAtZXF1aXY9IlgtVUEtQ29tcGF0aWJsZSIgY29udGVudD0iSUU9ZWRnZSIgLz4KICAgIDxzdHlsZSB0eXBlPSJ0ZXh0L2NzcyI+CiAgICAgICAgQG1lZGlhIHNjcmVlbiB7CiAgICAgICAgICAgIEBmb250LWZhY2UgewogICAgICAgICAgICAgICAgZm9udC1mYW1pbHk6ICcnTGF0bycnOwogICAgICAgICAgICAgICAgZm9udC1zdHlsZTogbm9ybWFsOwogICAgICAgICAgICAgICAgZm9udC13ZWlnaHQ6IDQwMDsKICAgICAgICAgICAgICAgIHNyYzogbG9jYWwoJydMYXRvIFJlZ3VsYXInJyksIGxvY2FsKCcnTGF0by1SZWd1bGFyJycpLCB1cmwoaHR0cHM6Ly9mb250cy5nc3RhdGljLmNvbS9zL2xhdG8vdjExL3FJSVlSVS1vUk9rSWs4dmZ2eHc2UXZlc1pXMnhPUS14c05xTzQ3bTU1REEud29mZikgZm9ybWF0KCcnd29mZicnKTsKICAgICAgICAgICAgfQoKICAgICAgICAgICAgQGZvbnQtZmFjZSB7CiAgICAgICAgICAgICAgICBmb250LWZhbWlseTogJydMYXRvJyc7CiAgICAgICAgICAgICAgICBmb250LXN0eWxlOiBub3JtYWw7CiAgICAgICAgICAgICAgICBmb250LXdlaWdodDogNzAwOwogICAgICAgICAgICAgICAgc3JjOiBsb2NhbCgnJ0xhdG8gQm9sZCcnKSwgbG9jYWwoJydMYXRvLUJvbGQnJyksIHVybChodHRwczovL2ZvbnRzLmdzdGF0aWMuY29tL3MvbGF0by92MTEvcWRnVUc0VTA5SG5Kd2hZSS11SzE4d0xVdUVwVHlvVXN0cUVtNUFNbEpvNC53b2ZmKSBmb3JtYXQoJyd3b2ZmJycpOwogICAgICAgICAgICB9CgogICAgICAgICAgICBAZm9udC1mYWNlIHsKICAgICAgICAgICAgICAgIGZvbnQtZmFtaWx5OiAnJ0xhdG8nJzsKICAgICAgICAgICAgICAgIGZvbnQtc3R5bGU6IGl0YWxpYzsKICAgICAgICAgICAgICAgIGZvbnQtd2VpZ2h0OiA0MDA7CiAgICAgICAgICAgICAgICBzcmM6IGxvY2FsKCcnTGF0byBJdGFsaWMnJyksIGxvY2FsKCcnTGF0by1JdGFsaWMnJyksIHVybChodHRwczovL2ZvbnRzLmdzdGF0aWMuY29tL3MvbGF0by92MTEvUll5Wk5vZUZnYjBsN1czVnUxYVNXT3Z2RGluMXBLOGFLdGVMcGVaNWMwQS53b2ZmKSBmb3JtYXQoJyd3b2ZmJycpOwogICAgICAgICAgICB9CgogICAgICAgICAgICBAZm9udC1mYWNlIHsKICAgICAgICAgICAgICAgIGZvbnQtZmFtaWx5OiAnJ0xhdG8nJzsKICAgICAgICAgICAgICAgIGZvbnQtc3R5bGU6IGl0YWxpYzsKICAgICAgICAgICAgICAgIGZvbnQtd2VpZ2h0OiA3MDA7CiAgICAgICAgICAgICAgICBzcmM6IGxvY2FsKCcnTGF0byBCb2xkIEl0YWxpYycnKSwgbG9jYWwoJydMYXRvLUJvbGRJdGFsaWMnJyksIHVybChodHRwczovL2ZvbnRzLmdzdGF0aWMuY29tL3MvbGF0by92MTEvSGtGX3FJMXhfbm94bHhocmhNUVlFTE8zTGRjQVpZV2w5U2k2dnZ4TC1xVS53b2ZmKSBmb3JtYXQoJyd3b2ZmJycpOwogICAgICAgICAgICB9CiAgICAgICAgfQoKICAgICAgICAvKiBDTElFTlQtU1BFQ0lGSUMgU1RZTEVTICovCiAgICAgICAgYm9keSwKICAgICAgICB0YWJsZSwKICAgICAgICB0ZCwKICAgICAgICBhIHsKICAgICAgICAgICAgLXdlYmtpdC10ZXh0LXNpemUtYWRqdXN0OiAxMDAlOwogICAgICAgICAgICAtbXMtdGV4dC1zaXplLWFkanVzdDogMTAwJTsKICAgICAgICB9CgogICAgICAgIHRhYmxlLAogICAgICAgIHRkIHsKICAgICAgICAgICAgbXNvLXRhYmxlLWxzcGFjZTogMHB0OwogICAgICAgICAgICBtc28tdGFibGUtcnNwYWNlOiAwcHQ7CiAgICAgICAgfQoKICAgICAgICBpbWcgewogICAgICAgICAgICAtbXMtaW50ZXJwb2xhdGlvbi1tb2RlOiBiaWN1YmljOwogICAgICAgIH0KCiAgICAgICAgLyogUkVTRVQgU1RZTEVTICovCiAgICAgICAgaW1nIHsKICAgICAgICAgICAgYm9yZGVyOiAwOwogICAgICAgICAgICBoZWlnaHQ6IGF1dG87CiAgICAgICAgICAgIGxpbmUtaGVpZ2h0OiAxMDAlOwogICAgICAgICAgICBvdXRsaW5lOiBub25lOwogICAgICAgICAgICB0ZXh0LWRlY29yYXRpb246IG5vbmU7CiAgICAgICAgfQoKICAgICAgICB0YWJsZSB7CiAgICAgICAgICAgIGJvcmRlci1jb2xsYXBzZTogY29sbGFwc2UgIWltcG9ydGFudDsKICAgICAgICB9CgogICAgICAgIGJvZHkgewogICAgICAgICAgICBoZWlnaHQ6IDEwMCUgIWltcG9ydGFudDsKICAgICAgICAgICAgbWFyZ2luOiAwICFpbXBvcnRhbnQ7CiAgICAgICAgICAgIHBhZGRpbmc6IDAgIWltcG9ydGFudDsKICAgICAgICAgICAgd2lkdGg6IDEwMCUgIWltcG9ydGFudDsKICAgICAgICB9CgogICAgICAgIC8qIGlPUyBCTFVFIExJTktTICovCiAgICAgICAgYXgtYXBwbGUtZGF0YS1kZXRlY3RvcnMgewogICAgICAgICAgICBjb2xvcjogaW5oZXJpdCAhaW1wb3J0YW50OwogICAgICAgICAgICB0ZXh0LWRlY29yYXRpb246IG5vbmUgIWltcG9ydGFudDsKICAgICAgICAgICAgZm9udC1zaXplOiBpbmhlcml0ICFpbXBvcnRhbnQ7CiAgICAgICAgICAgIGZvbnQtZmFtaWx5OiBpbmhlcml0ICFpbXBvcnRhbnQ7CiAgICAgICAgICAgIGZvbnQtd2VpZ2h0OiBpbmhlcml0ICFpbXBvcnRhbnQ7CiAgICAgICAgICAgIGxpbmUtaGVpZ2h0OiBpbmhlcml0ICFpbXBvcnRhbnQ7CiAgICAgICAgfQoKICAgICAgICAvKiBNT0JJTEUgU1RZTEVTICovCiAgICAgICAgQG1lZGlhIHNjcmVlbiBhbmQgKG1heC13aWR0aDo2MDBweCkgewogICAgICAgICAgICBoMSB7CiAgICAgICAgICAgICAgICBmb250LXNpemU6IDMycHggIWltcG9ydGFudDsKICAgICAgICAgICAgICAgIGxpbmUtaGVpZ2h0OiAzMnB4ICFpbXBvcnRhbnQ7CiAgICAgICAgICAgIH0KICAgICAgICB9CgogICAgICAgIC8qIEFORFJPSUQgQ0VOVEVSIEZJWCAqLwogICAgICAgIGRpdnN0eWxlKj0ibWFyZ2luOiAxNnB4IDA7IiB7CiAgICAgICAgICAgIG1hcmdpbjogMCAhaW1wb3J0YW50OwogICAgICAgIH0KICAgIDwvc3R5bGU+CjwvaGVhZD4KCjxib2R5IHN0eWxlPSJiYWNrZ3JvdW5kLWNvbG9yOiAjZjRmNGY0OyBtYXJnaW46IDAgIWltcG9ydGFudDsgcGFkZGluZzogMCAhaW1wb3J0YW50OyI+CiAgICA8IS0tIEhJRERFTiBQUkVIRUFERVIgVEVYVCAtLT4KICAgIDxkaXYgc3R5bGU9ImRpc3BsYXk6IG5vbmU7IGZvbnQtc2l6ZTogMXB4OyBjb2xvcjogI2ZlZmVmZTsgbGluZS1oZWlnaHQ6IDFweDsgZm9udC1mYW1pbHk6ICcnTGF0bycnLCBIZWx2ZXRpY2EsIEFyaWFsLCBzYW5zLXNlcmlmOyBtYXgtaGVpZ2h0OiAwcHg7IG1heC13aWR0aDogMHB4OyBvcGFjaXR5OiAwOyBvdmVyZmxvdzogaGlkZGVuOyI+IFdlIGFyZSB0aHJpbGxlZCB0byBoYXZlIHlvdSBoZXJlISBHZXQgcmVhZHkgdG8gZGl2ZSBpbnRvIHlvdXIgbmV3IGFjY291bnQuIDwvZGl2PgogICAgPHRhYmxlIGJvcmRlcj0iMCIgY2VsbHBhZGRpbmc9IjAiIGNlbGxzcGFjaW5nPSIwIiB3aWR0aD0iMTAwJSI+CiAgICAgICAgPCEtLSBMT0dPIC0tPgogICAgICAgIDx0cj4KICAgICAgICAgICAgPHRkIGJnY29sb3I9IiMzZjQ2NGQiIGFsaWduPSJjZW50ZXIiPgogICAgICAgICAgICAgICAgPHRhYmxlIGJvcmRlcj0iMCIgY2VsbHBhZGRpbmc9IjAiIGNlbGxzcGFjaW5nPSIwIiB3aWR0aD0iMTAwJSIgc3R5bGU9Im1heC13aWR0aDogNjAwcHg7Ij4KICAgICAgICAgICAgICAgICAgICA8dHI+CiAgICAgICAgICAgICAgICAgICAgICAgIDx0ZCBhbGlnbj0iY2VudGVyIiB2YWxpZ249InRvcCIgc3R5bGU9InBhZGRpbmc6IDQwcHggMTBweCA0MHB4IDEwcHg7Ij4gPC90ZD4KICAgICAgICAgICAgICAgICAgICA8L3RyPgogICAgICAgICAgICAgICAgPC90YWJsZT4KICAgICAgICAgICAgPC90ZD4KICAgICAgICA8L3RyPgogICAgICAgIDx0cj4KICAgICAgICAgICAgPHRkIGJnY29sb3I9IiMzZjQ2NGQiIGFsaWduPSJjZW50ZXIiIHN0eWxlPSJwYWRkaW5nOiAwcHggMTBweCAwcHggMTBweDsiPgogICAgICAgICAgICAgICAgPHRhYmxlIGJvcmRlcj0iMCIgY2VsbHBhZGRpbmc9IjAiIGNlbGxzcGFjaW5nPSIwIiB3aWR0aD0iMTAwJSIgc3R5bGU9Im1heC13aWR0aDogNjAwcHg7Ij4KICAgICAgICAgICAgICAgICAgICA8dHI+CiAgICAgICAgICAgICAgICAgICAgICAgIDx0ZCBiZ2NvbG9yPSIjZmZmZmZmIiBhbGlnbj0iY2VudGVyIiB2YWxpZ249InRvcCIgc3R5bGU9InBhZGRpbmc6IDQwcHggMjBweCAyMHB4IDIwcHg7IGJvcmRlci1yYWRpdXM6IDRweCA0cHggMHB4IDBweDsgY29sb3I6ICMxMTExMTE7IGZvbnQtZmFtaWx5OiAnJ0xhdG8nJywgSGVsdmV0aWNhLCBBcmlhbCwgc2Fucy1zZXJpZjsgZm9udC1zaXplOiA0OHB4OyBmb250LXdlaWdodDogNDAwOyBsZXR0ZXItc3BhY2luZzogNHB4OyBsaW5lLWhlaWdodDogNDhweDsiPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgPGltZyBzcmM9Imh0dHA6Ly93d3cubG90ZXJpYXRpYmlhbmEuY29tLmJyL2ltYWdlcy9Mb2dvMjEweDUwLnBuZyIgd2lkdGg9IjI1MCIgaGVpZ2h0PSIxNTAiIHN0eWxlPSJkaXNwbGF5OiBibG9jazsgYm9yZGVyOiAwcHg7IiAvPgogICAgICAgICAgICAgICAgICAgICAgICA8L3RkPgogICAgICAgICAgICAgICAgICAgIDwvdHI+CiAgICAgICAgICAgICAgICA8L3RhYmxlPgogICAgICAgICAgICA8L3RkPgogICAgICAgIDwvdHI+CiAgICAgICAgPHRyPgogICAgICAgICAgICA8dGQgYmdjb2xvcj0iI2Y0ZjRmNCIgYWxpZ249ImNlbnRlciIgc3R5bGU9InBhZGRpbmc6IDBweCAxMHB4IDBweCAxMHB4OyI+CiAgICAgICAgICAgICAgICA8dGFibGUgYm9yZGVyPSIwIiBjZWxscGFkZGluZz0iMCIgY2VsbHNwYWNpbmc9IjAiIHdpZHRoPSIxMDAlIiBzdHlsZT0ibWF4LXdpZHRoOiA2MDBweDsiPgogICAgICAgICAgICAgICAgICAgIDx0cj4KICAgICAgICAgICAgICAgICAgICAgICAgPHRkIGJnY29sb3I9IiNmZmZmZmYiIGFsaWduPSJsZWZ0IiBzdHlsZT0icGFkZGluZzogMjBweCAzMHB4IDQwcHggMzBweDsgY29sb3I6ICM2NjY2NjY7IGZvbnQtZmFtaWx5OiAnJ0xhdG8nJywgSGVsdmV0aWNhLCBBcmlhbCwgc2Fucy1zZXJpZjsgZm9udC1zaXplOiAxOHB4OyBmb250LXdlaWdodDogNDAwOyBsaW5lLWhlaWdodDogMjVweDsiPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgPHAgc3R5bGU9Im1hcmdpbjogMDsiPnt7VGV4dEJvZHl9fTwvcD4KICAgICAgICAgICAgICAgICAgICAgICAgPC90ZD4KICAgICAgICAgICAgICAgICAgICA8L3RyPgoJCQkJCTx0cj4KICAgICAgICAgICAgICAgICAgICAgICAgPHRkIGJnY29sb3I9IiNmZmZmZmYiIGFsaWduPSJsZWZ0IiBzdHlsZT0icGFkZGluZzogMjBweCAzMHB4IDQwcHggMzBweDsgY29sb3I6ICM2NjY2NjY7IGZvbnQtZmFtaWx5OiAnJ0xhdG8nJywgSGVsdmV0aWNhLCBBcmlhbCwgc2Fucy1zZXJpZjsgZm9udC1zaXplOiAxOHB4OyBmb250LXdlaWdodDogNDAwOyBsaW5lLWhlaWdodDogMjVweDsiPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgPHAgc3R5bGU9Im1hcmdpbjogMDsiPkVzdGFtb3MgZmVsaXplcyBwb3Igc2UgY2FkYXN0cmFyLiBQcmltZWlybywgdm9jw6ogcHJlY2lzYSBjb25maXJtYXIgc3VhIGNvbnRhLiBCYXN0YSBwcmVzc2lvbmFyIG8gYm90w6NvIGFiYWl4by48L3A+CiAgICAgICAgICAgICAgICAgICAgICAgIDwvdGQ+CiAgICAgICAgICAgICAgICAgICAgPC90cj4KICAgICAgICAgICAgICAgICAgICA8dHI+CiAgICAgICAgICAgICAgICAgICAgICAgIDx0ZCBiZ2NvbG9yPSIjZmZmZmZmIiBhbGlnbj0ibGVmdCI+CiAgICAgICAgICAgICAgICAgICAgICAgICAgICA8dGFibGUgd2lkdGg9IjEwMCUiIGJvcmRlcj0iMCIgY2VsbHNwYWNpbmc9IjAiIGNlbGxwYWRkaW5nPSIwIj4KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICA8dHI+CiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIDx0ZCBiZ2NvbG9yPSIjZmZmZmZmIiBhbGlnbj0iY2VudGVyIiBzdHlsZT0icGFkZGluZzogMjBweCAzMHB4IDYwcHggMzBweDsiPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgPHRhYmxlIGJvcmRlcj0iMCIgY2VsbHNwYWNpbmc9IjAiIGNlbGxwYWRkaW5nPSIwIj4KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICA8dHI+CiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIDx0ZCBhbGlnbj0iY2VudGVyIiBzdHlsZT0iYm9yZGVyLXJhZGl1czogM3B4OyIgYmdjb2xvcj0iI0ZGRjAxIj4KCQkJCQkJCQkJCQkJCTxhIGhyZWY9Int7QnV0dG9uTGlua319IiB0YXJnZXQ9Il9ibGFuayIgc3R5bGU9ImZvbnQtc2l6ZTogMjBweDsgZm9udC1mYW1pbHk6IEhlbHZldGljYSwgQXJpYWwsIHNhbnMtc2VyaWY7IHRleHQtZGVjb3JhdGlvbjogbm9uZTsgY29sb3I6ICM5OTk5OTk7IHRleHQtZGVjb3JhdGlvbjogbm9uZTsgcGFkZGluZzogMTVweCAyNXB4OyBib3JkZXItcmFkaXVzOiAycHg7IGJvcmRlcjogMXB4IHNvbGlkICM5OTk5OTk7IGRpc3BsYXk6IGlubGluZS1ibG9jazsiPkNvbmZpcm1hcjwvYT4KCQkJCQkJCQkJCQkJPC90ZD4KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICA8L3RyPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgPC90YWJsZT4KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgPC90ZD4KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICA8L3RyPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgPC90YWJsZT4KICAgICAgICAgICAgICAgICAgICAgICAgPC90ZD4KICAgICAgICAgICAgICAgICAgICA8L3RyPiA8IS0tIENPUFkgLS0+CiAgICAgICAgICAgICAgICAgICAgPHRyPgogICAgICAgICAgICAgICAgICAgICAgICA8dGQgYmdjb2xvcj0iI2ZmZmZmZiIgYWxpZ249ImxlZnQiIHN0eWxlPSJwYWRkaW5nOiAwcHggMzBweCAwcHggMzBweDsgY29sb3I6ICM2NjY2NjY7IGZvbnQtZmFtaWx5OiAnJ0xhdG8nJywgSGVsdmV0aWNhLCBBcmlhbCwgc2Fucy1zZXJpZjsgZm9udC1zaXplOiAxOHB4OyBmb250LXdlaWdodDogNDAwOyBsaW5lLWhlaWdodDogMjVweDsiPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgPHAgc3R5bGU9Im1hcmdpbjogMDsiPkNhc28gaXNzbyBuw6NvIGZ1bmNpb25lLCBjb3BpZSBlIGNvbGUgbyBzZWd1aW50ZSBsaW5rIG5vIHNldSBuYXZlZ2Fkb3I6PC9wPgogICAgICAgICAgICAgICAgICAgICAgICA8L3RkPgogICAgICAgICAgICAgICAgICAgIDwvdHI+IDwhLS0gQ09QWSAtLT4KICAgICAgICAgICAgICAgICAgICA8dHI+CiAgICAgICAgICAgICAgICAgICAgICAgIDx0ZCBiZ2NvbG9yPSIjZmZmZmZmIiBhbGlnbj0ibGVmdCIgc3R5bGU9InBhZGRpbmc6IDIwcHggMzBweCAyMHB4IDMwcHg7IGNvbG9yOiAjNjY2NjY2OyBmb250LWZhbWlseTogJydMYXRvJycsIEhlbHZldGljYSwgQXJpYWwsIHNhbnMtc2VyaWY7IGZvbnQtc2l6ZTogMThweDsgZm9udC13ZWlnaHQ6IDQwMDsgbGluZS1oZWlnaHQ6IDI1cHg7Ij4KICAgICAgICAgICAgICAgICAgICAgICAgICAgIDxwIHN0eWxlPSJtYXJnaW46IDA7IHdpZHRoOjUwMHB4OyB3b3JkLXdyYXA6IGJyZWFrLXdvcmQ7Ij4KCQkJCQkJCQl7e1RleHRMaW5rQm9keX19CgkJCQkJCQk8L3A+CiAgICAgICAgICAgICAgICAgICAgICAgIDwvdGQ+CiAgICAgICAgICAgICAgICAgICAgPC90cj4KICAgICAgICAgICAgICAgICAgICA8dHI+CiAgICAgICAgICAgICAgICAgICAgICAgIDx0ZCBiZ2NvbG9yPSIjZmZmZmZmIiBhbGlnbj0ibGVmdCIgc3R5bGU9InBhZGRpbmc6IDBweCAzMHB4IDIwcHggMzBweDsgY29sb3I6ICM2NjY2NjY7IGZvbnQtZmFtaWx5OiAnJ0xhdG8nJywgSGVsdmV0aWNhLCBBcmlhbCwgc2Fucy1zZXJpZjsgZm9udC1zaXplOiAxOHB4OyBmb250LXdlaWdodDogNDAwOyBsaW5lLWhlaWdodDogMjVweDsiPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgPHAgc3R5bGU9Im1hcmdpbjogMDsiPkNhc28gdGVuaGEgYWxndW1hIGTDunZpZGEsIHJlc3BvbmRhIGEgZXN0ZSBlLW1haWwuIEVzdGFtb3Mgc2VtcHJlIGZlbGl6ZXMgZW0gYWp1ZGFyLjwvcD4KICAgICAgICAgICAgICAgICAgICAgICAgPC90ZD4KICAgICAgICAgICAgICAgICAgICA8L3RyPgogICAgICAgICAgICAgICAgICAgIDx0cj4KICAgICAgICAgICAgICAgICAgICAgICAgPHRkIGJnY29sb3I9IiNmZmZmZmYiIGFsaWduPSJsZWZ0IiBzdHlsZT0icGFkZGluZzogMHB4IDMwcHggNDBweCAzMHB4OyBib3JkZXItcmFkaXVzOiAwcHggMHB4IDRweCA0cHg7IGNvbG9yOiAjNjY2NjY2OyBmb250LWZhbWlseTogJydMYXRvJycsIEhlbHZldGljYSwgQXJpYWwsIHNhbnMtc2VyaWY7IGZvbnQtc2l6ZTogMThweDsgZm9udC13ZWlnaHQ6IDQwMDsgbGluZS1oZWlnaHQ6IDI1cHg7Ij4KICAgICAgICAgICAgICAgICAgICAgICAgICAgIDxwIHN0eWxlPSJtYXJnaW46IDA7Ij5TYXVkYcOnw7Vlcyw8YnI+RXF1aXBlIFRDIExvdHRlcnk8L3A+CiAgICAgICAgICAgICAgICAgICAgICAgIDwvdGQ+CiAgICAgICAgICAgICAgICAgICAgPC90cj4KICAgICAgICAgICAgICAgIDwvdGFibGU+CiAgICAgICAgICAgIDwvdGQ+CiAgICAgICAgPC90cj4KICAgIDwvdGFibGU+CjwvYm9keT4KCjwvaHRtbD4nLCAnVGVtcGxhdGUgcGFkcsOjbyBlbnZpbyBkZSBlbWFpbCAnLCAxKTsKCgoKCgoKSU5TRVJUIElOVE8gcHVibGljLiJUQl9QQVJBTUVUUk9TX1NJU1RFTUEiKCJEQ19OQU1FIiwgIkRDX1ZBTE9SIiwgIkRDX0RFU0NSSUNBTyIsICJJRF9USVBPX0NBTVBPIikgVkFMVUVTICgnSHRtbFRlbXBsYXRlRW1haWxSZXNldFNlbmhhJywgJzwhRE9DVFlQRSBodG1sPgo8aHRtbD4KCjxoZWFkPgogICAgPHRpdGxlPjwvdGl0bGU+CiAgICA8bWV0YSBodHRwLWVxdWl2PSJDb250ZW50LVR5cGUiIGNvbnRlbnQ9InRleHQvaHRtbDsgY2hhcnNldD11dGYtOCIgLz4KICAgIDxtZXRhIG5hbWU9InZpZXdwb3J0IiBjb250ZW50PSJ3aWR0aD1kZXZpY2Utd2lkdGgsIGluaXRpYWwtc2NhbGU9MSI+CiAgICA8bWV0YSBodHRwLWVxdWl2PSJYLVVBLUNvbXBhdGlibGUiIGNvbnRlbnQ9IklFPWVkZ2UiIC8+CiAgICA8c3R5bGUgdHlwZT0idGV4dC9jc3MiPgogICAgICAgIEBtZWRpYSBzY3JlZW4gewogICAgICAgICAgICBAZm9udC1mYWNlIHsKICAgICAgICAgICAgICAgIGZvbnQtZmFtaWx5OiAnJ0xhdG8nJzsKICAgICAgICAgICAgICAgIGZvbnQtc3R5bGU6IG5vcm1hbDsKICAgICAgICAgICAgICAgIGZvbnQtd2VpZ2h0OiA0MDA7CiAgICAgICAgICAgICAgICBzcmM6IGxvY2FsKCcnTGF0byBSZWd1bGFyJycpLCBsb2NhbCgnJ0xhdG8tUmVndWxhcicnKSwgdXJsKGh0dHBzOi8vZm9udHMuZ3N0YXRpYy5jb20vcy9sYXRvL3YxMS9xSUlZUlUtb1JPa0lrOHZmdnh3NlF2ZXNaVzJ4T1EteHNOcU80N201NURBLndvZmYpIGZvcm1hdCgnJ3dvZmYnJyk7CiAgICAgICAgICAgIH0KCiAgICAgICAgICAgIEBmb250LWZhY2UgewogICAgICAgICAgICAgICAgZm9udC1mYW1pbHk6ICcnTGF0bycnOwogICAgICAgICAgICAgICAgZm9udC1zdHlsZTogbm9ybWFsOwogICAgICAgICAgICAgICAgZm9udC13ZWlnaHQ6IDcwMDsKICAgICAgICAgICAgICAgIHNyYzogbG9jYWwoJydMYXRvIEJvbGQnJyksIGxvY2FsKCcnTGF0by1Cb2xkJycpLCB1cmwoaHR0cHM6Ly9mb250cy5nc3RhdGljLmNvbS9zL2xhdG8vdjExL3FkZ1VHNFUwOUhuSndoWUktdUsxOHdMVXVFcFR5b1VzdHFFbTVBTWxKbzQud29mZikgZm9ybWF0KCcnd29mZicnKTsKICAgICAgICAgICAgfQoKICAgICAgICAgICAgQGZvbnQtZmFjZSB7CiAgICAgICAgICAgICAgICBmb250LWZhbWlseTogJydMYXRvJyc7CiAgICAgICAgICAgICAgICBmb250LXN0eWxlOiBpdGFsaWM7CiAgICAgICAgICAgICAgICBmb250LXdlaWdodDogNDAwOwogICAgICAgICAgICAgICAgc3JjOiBsb2NhbCgnJ0xhdG8gSXRhbGljJycpLCBsb2NhbCgnJ0xhdG8tSXRhbGljJycpLCB1cmwoaHR0cHM6Ly9mb250cy5nc3RhdGljLmNvbS9zL2xhdG8vdjExL1JZeVpOb2VGZ2IwbDdXM1Z1MWFTV092dkRpbjFwSzhhS3RlTHBlWjVjMEEud29mZikgZm9ybWF0KCcnd29mZicnKTsKICAgICAgICAgICAgfQoKICAgICAgICAgICAgQGZvbnQtZmFjZSB7CiAgICAgICAgICAgICAgICBmb250LWZhbWlseTogJydMYXRvJyc7CiAgICAgICAgICAgICAgICBmb250LXN0eWxlOiBpdGFsaWM7CiAgICAgICAgICAgICAgICBmb250LXdlaWdodDogNzAwOwogICAgICAgICAgICAgICAgc3JjOiBsb2NhbCgnJ0xhdG8gQm9sZCBJdGFsaWMnJyksIGxvY2FsKCcnTGF0by1Cb2xkSXRhbGljJycpLCB1cmwoaHR0cHM6Ly9mb250cy5nc3RhdGljLmNvbS9zL2xhdG8vdjExL0hrRl9xSTF4X25veGx4aHJoTVFZRUxPM0xkY0FaWVdsOVNpNnZ2eEwtcVUud29mZikgZm9ybWF0KCcnd29mZicnKTsKICAgICAgICAgICAgfQogICAgICAgIH0KCiAgICAgICAgLyogQ0xJRU5ULVNQRUNJRklDIFNUWUxFUyAqLwogICAgICAgIGJvZHksCiAgICAgICAgdGFibGUsCiAgICAgICAgdGQsCiAgICAgICAgYSB7CiAgICAgICAgICAgIC13ZWJraXQtdGV4dC1zaXplLWFkanVzdDogMTAwJTsKICAgICAgICAgICAgLW1zLXRleHQtc2l6ZS1hZGp1c3Q6IDEwMCU7CiAgICAgICAgfQoKICAgICAgICB0YWJsZSwKICAgICAgICB0ZCB7CiAgICAgICAgICAgIG1zby10YWJsZS1sc3BhY2U6IDBwdDsKICAgICAgICAgICAgbXNvLXRhYmxlLXJzcGFjZTogMHB0OwogICAgICAgIH0KCiAgICAgICAgaW1nIHsKICAgICAgICAgICAgLW1zLWludGVycG9sYXRpb24tbW9kZTogYmljdWJpYzsKICAgICAgICB9CgogICAgICAgIC8qIFJFU0VUIFNUWUxFUyAqLwogICAgICAgIGltZyB7CiAgICAgICAgICAgIGJvcmRlcjogMDsKICAgICAgICAgICAgaGVpZ2h0OiBhdXRvOwogICAgICAgICAgICBsaW5lLWhlaWdodDogMTAwJTsKICAgICAgICAgICAgb3V0bGluZTogbm9uZTsKICAgICAgICAgICAgdGV4dC1kZWNvcmF0aW9uOiBub25lOwogICAgICAgIH0KCiAgICAgICAgdGFibGUgewogICAgICAgICAgICBib3JkZXItY29sbGFwc2U6IGNvbGxhcHNlICFpbXBvcnRhbnQ7CiAgICAgICAgfQoKICAgICAgICBib2R5IHsKICAgICAgICAgICAgaGVpZ2h0OiAxMDAlICFpbXBvcnRhbnQ7CiAgICAgICAgICAgIG1hcmdpbjogMCAhaW1wb3J0YW50OwogICAgICAgICAgICBwYWRkaW5nOiAwICFpbXBvcnRhbnQ7CiAgICAgICAgICAgIHdpZHRoOiAxMDAlICFpbXBvcnRhbnQ7CiAgICAgICAgfQoKICAgICAgICAvKiBpT1MgQkxVRSBMSU5LUyAqLwogICAgICAgIGF4LWFwcGxlLWRhdGEtZGV0ZWN0b3JzIHsKICAgICAgICAgICAgY29sb3I6IGluaGVyaXQgIWltcG9ydGFudDsKICAgICAgICAgICAgdGV4dC1kZWNvcmF0aW9uOiBub25lICFpbXBvcnRhbnQ7CiAgICAgICAgICAgIGZvbnQtc2l6ZTogaW5oZXJpdCAhaW1wb3J0YW50OwogICAgICAgICAgICBmb250LWZhbWlseTogaW5oZXJpdCAhaW1wb3J0YW50OwogICAgICAgICAgICBmb250LXdlaWdodDogaW5oZXJpdCAhaW1wb3J0YW50OwogICAgICAgICAgICBsaW5lLWhlaWdodDogaW5oZXJpdCAhaW1wb3J0YW50OwogICAgICAgIH0KCiAgICAgICAgLyogTU9CSUxFIFNUWUxFUyAqLwogICAgICAgIEBtZWRpYSBzY3JlZW4gYW5kIChtYXgtd2lkdGg6NjAwcHgpIHsKICAgICAgICAgICAgaDEgewogICAgICAgICAgICAgICAgZm9udC1zaXplOiAzMnB4ICFpbXBvcnRhbnQ7CiAgICAgICAgICAgICAgICBsaW5lLWhlaWdodDogMzJweCAhaW1wb3J0YW50OwogICAgICAgICAgICB9CiAgICAgICAgfQoKICAgICAgICAvKiBBTkRST0lEIENFTlRFUiBGSVggKi8KICAgICAgICBkaXZzdHlsZSo9Im1hcmdpbjogMTZweCAwOyIgewogICAgICAgICAgICBtYXJnaW46IDAgIWltcG9ydGFudDsKICAgICAgICB9CiAgICA8L3N0eWxlPgo8L2hlYWQ+Cgo8Ym9keSBzdHlsZT0iYmFja2dyb3VuZC1jb2xvcjogI2Y0ZjRmNDsgbWFyZ2luOiAwICFpbXBvcnRhbnQ7IHBhZGRpbmc6IDAgIWltcG9ydGFudDsiPgogICAgPCEtLSBISURERU4gUFJFSEVBREVSIFRFWFQgLS0+CiAgICA8ZGl2IHN0eWxlPSJkaXNwbGF5OiBub25lOyBmb250LXNpemU6IDFweDsgY29sb3I6ICNmZWZlZmU7IGxpbmUtaGVpZ2h0OiAxcHg7IGZvbnQtZmFtaWx5OiAnJ0xhdG8nJywgSGVsdmV0aWNhLCBBcmlhbCwgc2Fucy1zZXJpZjsgbWF4LWhlaWdodDogMHB4OyBtYXgtd2lkdGg6IDBweDsgb3BhY2l0eTogMDsgb3ZlcmZsb3c6IGhpZGRlbjsiPiBXZScncmUgdGhyaWxsZWQgdG8gaGF2ZSB5b3UgaGVyZSEgR2V0IHJlYWR5IHRvIGRpdmUgaW50byB5b3VyIG5ldyBhY2NvdW50LiA8L2Rpdj4KICAgIDx0YWJsZSBib3JkZXI9IjAiIGNlbGxwYWRkaW5nPSIwIiBjZWxsc3BhY2luZz0iMCIgd2lkdGg9IjEwMCUiPgogICAgICAgIDwhLS0gTE9HTyAtLT4KICAgICAgICA8dHI+CiAgICAgICAgICAgIDx0ZCBiZ2NvbG9yPSIjM2Y0NjRkIiBhbGlnbj0iY2VudGVyIj4KICAgICAgICAgICAgICAgIDx0YWJsZSBib3JkZXI9IjAiIGNlbGxwYWRkaW5nPSIwIiBjZWxsc3BhY2luZz0iMCIgd2lkdGg9IjEwMCUiIHN0eWxlPSJtYXgtd2lkdGg6IDYwMHB4OyI+CiAgICAgICAgICAgICAgICAgICAgPHRyPgogICAgICAgICAgICAgICAgICAgICAgICA8dGQgYWxpZ249ImNlbnRlciIgdmFsaWduPSJ0b3AiIHN0eWxlPSJwYWRkaW5nOiA0MHB4IDEwcHggNDBweCAxMHB4OyI+IDwvdGQ+CiAgICAgICAgICAgICAgICAgICAgPC90cj4KICAgICAgICAgICAgICAgIDwvdGFibGU+CiAgICAgICAgICAgIDwvdGQ+CiAgICAgICAgPC90cj4KICAgICAgICA8dHI+CiAgICAgICAgICAgIDx0ZCBiZ2NvbG9yPSIjM2Y0NjRkIiBhbGlnbj0iY2VudGVyIiBzdHlsZT0icGFkZGluZzogMHB4IDEwcHggMHB4IDEwcHg7Ij4KICAgICAgICAgICAgICAgIDx0YWJsZSBib3JkZXI9IjAiIGNlbGxwYWRkaW5nPSIwIiBjZWxsc3BhY2luZz0iMCIgd2lkdGg9IjEwMCUiIHN0eWxlPSJtYXgtd2lkdGg6IDYwMHB4OyI+CiAgICAgICAgICAgICAgICAgICAgPHRyPgogICAgICAgICAgICAgICAgICAgICAgICA8dGQgYmdjb2xvcj0iI2ZmZmZmZiIgYWxpZ249ImNlbnRlciIgdmFsaWduPSJ0b3AiIHN0eWxlPSJwYWRkaW5nOiA0MHB4IDIwcHggMjBweCAyMHB4OyBib3JkZXItcmFkaXVzOiA0cHggNHB4IDBweCAwcHg7IGNvbG9yOiAjMTExMTExOyBmb250LWZhbWlseTogJydMYXRvJycsIEhlbHZldGljYSwgQXJpYWwsIHNhbnMtc2VyaWY7IGZvbnQtc2l6ZTogNDhweDsgZm9udC13ZWlnaHQ6IDQwMDsgbGV0dGVyLXNwYWNpbmc6IDRweDsgbGluZS1oZWlnaHQ6IDQ4cHg7Ij4KICAgICAgICAgICAgICAgICAgICAgICAgICAgIDxpbWcgc3JjPSJodHRwOi8vd3d3LmxvdGVyaWF0aWJpYW5hLmNvbS5ici9pbWFnZXMvTG9nbzIxMHg1MC5wbmciIHdpZHRoPSIyNTAiIGhlaWdodD0iMTUwIiBzdHlsZT0iZGlzcGxheTogYmxvY2s7IGJvcmRlcjogMHB4OyIgLz4KICAgICAgICAgICAgICAgICAgICAgICAgPC90ZD4KICAgICAgICAgICAgICAgICAgICA8L3RyPgogICAgICAgICAgICAgICAgPC90YWJsZT4KICAgICAgICAgICAgPC90ZD4KICAgICAgICA8L3RyPgogICAgICAgIDx0cj4KICAgICAgICAgICAgPHRkIGJnY29sb3I9IiNmNGY0ZjQiIGFsaWduPSJjZW50ZXIiIHN0eWxlPSJwYWRkaW5nOiAwcHggMTBweCAwcHggMTBweDsiPgogICAgICAgICAgICAgICAgPHRhYmxlIGJvcmRlcj0iMCIgY2VsbHBhZGRpbmc9IjAiIGNlbGxzcGFjaW5nPSIwIiB3aWR0aD0iMTAwJSIgc3R5bGU9Im1heC13aWR0aDogNjAwcHg7Ij4KICAgICAgICAgICAgICAgICAgICA8dHI+CiAgICAgICAgICAgICAgICAgICAgICAgIDx0ZCBiZ2NvbG9yPSIjZmZmZmZmIiBhbGlnbj0ibGVmdCIgc3R5bGU9InBhZGRpbmc6IDIwcHggMzBweCA0MHB4IDMwcHg7IGNvbG9yOiAjNjY2NjY2OyBmb250LWZhbWlseTogJydMYXRvJycsIEhlbHZldGljYSwgQXJpYWwsIHNhbnMtc2VyaWY7IGZvbnQtc2l6ZTogMThweDsgZm9udC13ZWlnaHQ6IDQwMDsgbGluZS1oZWlnaHQ6IDI1cHg7Ij4KICAgICAgICAgICAgICAgICAgICAgICAgICAgIDxwIHN0eWxlPSJtYXJnaW46IDA7Ij57e1RleHRCb2R5fX08L3A+CiAgICAgICAgICAgICAgICAgICAgICAgIDwvdGQ+CiAgICAgICAgICAgICAgICAgICAgPC90cj4KCQkJCQk8dHI+CiAgICAgICAgICAgICAgICAgICAgICAgIDx0ZCBiZ2NvbG9yPSIjZmZmZmZmIiBhbGlnbj0ibGVmdCIgc3R5bGU9InBhZGRpbmc6IDIwcHggMzBweCA0MHB4IDMwcHg7IGNvbG9yOiAjNjY2NjY2OyBmb250LWZhbWlseTogJydMYXRvJycsIEhlbHZldGljYSwgQXJpYWwsIHNhbnMtc2VyaWY7IGZvbnQtc2l6ZTogMThweDsgZm9udC13ZWlnaHQ6IDQwMDsgbGluZS1oZWlnaHQ6IDI1cHg7Ij4KICAgICAgICAgICAgICAgICAgICAgICAgICAgIDxwIHN0eWxlPSJtYXJnaW46IDA7Ij5Fc3RhbW9zIGZlbGl6ZXMgcG9yIHNlIGNhZGFzdHJhci4gUHJpbWVpcm8sIHZvY8OqIHByZWNpc2EgY29uZmlybWFyIHN1YSBjb250YS4gQmFzdGEgcHJlc3Npb25hciBvIGJvdMOjbyBhYmFpeG8uPC9wPgogICAgICAgICAgICAgICAgICAgICAgICA8L3RkPgogICAgICAgICAgICAgICAgICAgIDwvdHI+CiAgICAgICAgICAgICAgICAgICAgPHRyPgogICAgICAgICAgICAgICAgICAgICAgICA8dGQgYmdjb2xvcj0iI2ZmZmZmZiIgYWxpZ249ImxlZnQiPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgPHRhYmxlIHdpZHRoPSIxMDAlIiBib3JkZXI9IjAiIGNlbGxzcGFjaW5nPSIwIiBjZWxscGFkZGluZz0iMCI+CiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgPHRyPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICA8dGQgYmdjb2xvcj0iI2ZmZmZmZiIgYWxpZ249ImNlbnRlciIgc3R5bGU9InBhZGRpbmc6IDIwcHggMzBweCA2MHB4IDMwcHg7Ij4KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIDx0YWJsZSBib3JkZXI9IjAiIGNlbGxzcGFjaW5nPSIwIiBjZWxscGFkZGluZz0iMCI+CiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgPHRyPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICA8dGQgYWxpZ249ImNlbnRlciIgc3R5bGU9ImJvcmRlci1yYWRpdXM6IDNweDsiIGJnY29sb3I9IiNGRkYwMSI+CgkJCQkJCQkJCQkJCQk8YSBocmVmPSJ7e0J1dHRvbkxpbmt9fSIgdGFyZ2V0PSJfYmxhbmsiIHN0eWxlPSJmb250LXNpemU6IDIwcHg7IGZvbnQtZmFtaWx5OiBIZWx2ZXRpY2EsIEFyaWFsLCBzYW5zLXNlcmlmOyB0ZXh0LWRlY29yYXRpb246IG5vbmU7IGNvbG9yOiAjOTk5OTk5OyB0ZXh0LWRlY29yYXRpb246IG5vbmU7IHBhZGRpbmc6IDE1cHggMjVweDsgYm9yZGVyLXJhZGl1czogMnB4OyBib3JkZXI6IDFweCBzb2xpZCAjOTk5OTk5OyBkaXNwbGF5OiBpbmxpbmUtYmxvY2s7Ij57e0J1dHRvblRleHR9fTwvYT4KCQkJCQkJCQkJCQkJPC90ZD4KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICA8L3RyPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgPC90YWJsZT4KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgPC90ZD4KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICA8L3RyPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgPC90YWJsZT4KICAgICAgICAgICAgICAgICAgICAgICAgPC90ZD4KICAgICAgICAgICAgICAgICAgICA8L3RyPiA8IS0tIENPUFkgLS0+CiAgICAgICAgICAgICAgICAgICAgPHRyPgogICAgICAgICAgICAgICAgICAgICAgICA8dGQgYmdjb2xvcj0iI2ZmZmZmZiIgYWxpZ249ImxlZnQiIHN0eWxlPSJwYWRkaW5nOiAwcHggMzBweCAwcHggMzBweDsgY29sb3I6ICM2NjY2NjY7IGZvbnQtZmFtaWx5OiAnJ0xhdG8nJywgSGVsdmV0aWNhLCBBcmlhbCwgc2Fucy1zZXJpZjsgZm9udC1zaXplOiAxOHB4OyBmb250LXdlaWdodDogNDAwOyBsaW5lLWhlaWdodDogMjVweDsiPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgPHAgc3R5bGU9Im1hcmdpbjogMDsiPkNhc28gaXNzbyBuw6NvIGZ1bmNpb25lLCBjb3BpZSBlIGNvbGUgbyBzZWd1aW50ZSBsaW5rIG5vIHNldSBuYXZlZ2Fkb3I6PC9wPgogICAgICAgICAgICAgICAgICAgICAgICA8L3RkPgogICAgICAgICAgICAgICAgICAgIDwvdHI+IDwhLS0gQ09QWSAtLT4KICAgICAgICAgICAgICAgICAgICA8dHI+CiAgICAgICAgICAgICAgICAgICAgICAgIDx0ZCBiZ2NvbG9yPSIjZmZmZmZmIiBhbGlnbj0ibGVmdCIgc3R5bGU9InBhZGRpbmc6IDIwcHggMzBweCAyMHB4IDMwcHg7IGNvbG9yOiAjNjY2NjY2OyBmb250LWZhbWlseTogJydMYXRvJycsIEhlbHZldGljYSwgQXJpYWwsIHNhbnMtc2VyaWY7IGZvbnQtc2l6ZTogMThweDsgZm9udC13ZWlnaHQ6IDQwMDsgbGluZS1oZWlnaHQ6IDI1cHg7Ij4KICAgICAgICAgICAgICAgICAgICAgICAgICAgIDxwIHN0eWxlPSJtYXJnaW46IDA7IHdpZHRoOjIwMHB4OyB3b3JkLXdyYXA6IGJyZWFrLXdvcmQ7Ij4KCQkJCQkJCQl7e1RleHRMaW5rQm9keX19CgkJCQkJCQk8L3A+CiAgICAgICAgICAgICAgICAgICAgICAgIDwvdGQ+CiAgICAgICAgICAgICAgICAgICAgPC90cj4KICAgICAgICAgICAgICAgICAgICA8dHI+CiAgICAgICAgICAgICAgICAgICAgICAgIDx0ZCBiZ2NvbG9yPSIjZmZmZmZmIiBhbGlnbj0ibGVmdCIgc3R5bGU9InBhZGRpbmc6IDBweCAzMHB4IDIwcHggMzBweDsgY29sb3I6ICM2NjY2NjY7IGZvbnQtZmFtaWx5OiAnJ0xhdG8nJywgSGVsdmV0aWNhLCBBcmlhbCwgc2Fucy1zZXJpZjsgZm9udC1zaXplOiAxOHB4OyBmb250LXdlaWdodDogNDAwOyBsaW5lLWhlaWdodDogMjVweDsiPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgPHAgc3R5bGU9Im1hcmdpbjogMDsiPkNhc28gdGVuaGEgYWxndW1hIGTDunZpZGEsIHJlc3BvbmRhIGEgZXN0ZSBlLW1haWwuIEVzdGFtb3Mgc2VtcHJlIGZlbGl6ZXMgZW0gYWp1ZGFyLjwvcD4KICAgICAgICAgICAgICAgICAgICAgICAgPC90ZD4KICAgICAgICAgICAgICAgICAgICA8L3RyPgogICAgICAgICAgICAgICAgICAgIDx0cj4KICAgICAgICAgICAgICAgICAgICAgICAgPHRkIGJnY29sb3I9IiNmZmZmZmYiIGFsaWduPSJsZWZ0IiBzdHlsZT0icGFkZGluZzogMHB4IDMwcHggNDBweCAzMHB4OyBib3JkZXItcmFkaXVzOiAwcHggMHB4IDRweCA0cHg7IGNvbG9yOiAjNjY2NjY2OyBmb250LWZhbWlseTogJydMYXRvJycsIEhlbHZldGljYSwgQXJpYWwsIHNhbnMtc2VyaWY7IGZvbnQtc2l6ZTogMThweDsgZm9udC13ZWlnaHQ6IDQwMDsgbGluZS1oZWlnaHQ6IDI1cHg7Ij4KICAgICAgICAgICAgICAgICAgICAgICAgICAgIDxwIHN0eWxlPSJtYXJnaW46IDA7Ij5TYXVkYcOnw7Vlczxicj57e1RleHRGaW5pc2h9fTwvcD4KICAgICAgICAgICAgICAgICAgICAgICAgPC90ZD4KICAgICAgICAgICAgICAgICAgICA8L3RyPgogICAgICAgICAgICAgICAgPC90YWJsZT4KICAgICAgICAgICAgPC90ZD4KICAgICAgICA8L3RyPgogICAgPC90YWJsZT4KPC9ib2R5PgoKPC9odG1sPg==" },
                    { new Guid("4c7cf579-c33d-47dc-b2aa-6f8a103d596b"), "logo para email", "LogoEmail", 1, "data:image/jpeg;base64,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" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "TB_PRODUTO",
                columns: new[] { "CD_PRODUTO", "DC_DESCRICAO", "DC_LINK", "MO_VALOR" },
                values: new object[,]
                {
                    { 10, "Aplicativo Teste 010", "http://localhost", 500m },
                    { 1, "Aplicativo Teste 001", "http://localhost", 500m },
                    { 2, "Aplicativo Teste 002", "http://localhost", 100m },
                    { 3, "Aplicativo Teste 003", "http://localhost", 300m },
                    { 4, "Aplicativo Teste 004", "http://localhost", 800m },
                    { 5, "Aplicativo Teste 005", "http://localhost", 500m },
                    { 6, "Aplicativo Teste 006", "http://localhost", 500m },
                    { 7, "Aplicativo Teste 007", "http://localhost", 500m },
                    { 8, "Aplicativo Teste 008", "http://localhost", 500m },
                    { 9, "Aplicativo Teste 009", "http://localhost", 500m },
                    { 11, "Aplicativo Teste 011", "http://localhost", 500m }
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
                name: "IX_AspNetUsers_EnderecoCodigoEndereco",
                schema: "dbo",
                table: "AspNetUsers",
                column: "EnderecoCodigoEndereco");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "dbo",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "dbo",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PEDIDO_CD_FORMA_PAGAMENTO",
                schema: "dbo",
                table: "TB_PEDIDO",
                column: "CD_FORMA_PAGAMENTO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PEDIDO_PedidoPagamentoCodigoPedidoPagamento",
                schema: "dbo",
                table: "TB_PEDIDO",
                column: "PedidoPagamentoCodigoPedidoPagamento");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PEDIDO_UsuarioId",
                schema: "dbo",
                table: "TB_PEDIDO",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PEDIDO_ITEM_PedidoCodigoPedido",
                schema: "dbo",
                table: "TB_PEDIDO_ITEM",
                column: "PedidoCodigoPedido");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PEDIDO_PAGAMENTO_PedidoPagamentoHistoricoCodigoPedidoPagamentoHistorico_PedidoPagamentoHistoricoCodigoPedidoPagamento",
                schema: "dbo",
                table: "TB_PEDIDO_PAGAMENTO",
                columns: new[] { "PedidoPagamentoHistoricoCodigoPedidoPagamentoHistorico", "PedidoPagamentoHistoricoCodigoPedidoPagamento" });
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
                name: "TB_EMAIL_SISTEMA",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TB_LOG_ERRO",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TB_PARAMETROS_SISTEMA",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TB_PEDIDO_HISTORICO",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TB_PEDIDO_ITEM",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TB_PESSOA_CARTAO",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TB_PRODUTO",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "TB_PEDIDO",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TB_FORMA_PAGAMENTO",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TB_PEDIDO_PAGAMENTO",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TB_PEDIDO_PAGAMENTO_HISTORICO",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TB_PESSOA_ENDERECO",
                schema: "dbo");
        }
    }
}
