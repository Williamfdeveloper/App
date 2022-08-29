using App.Domain.Contracts;
using App.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Service
{
    public class PedidoService : IPedidoService
    {
        private UserManager<Usuario> _userManager;
        private readonly ILogger<PedidoService> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ILoggerService _loggerService;
        private readonly IProdutoService _produtoService;
        private readonly IFormasPagamentoService _formasPagamentoService;
        //private ISession _session => _httpContextAccessor.HttpContext.Session;

        public PedidoService(
            ILoggerService loggerService,
            UserManager<Usuario> usrMgr,
            ILogger<PedidoService> logger,
            RoleManager<IdentityRole> RoleManager,
            SignInManager<Usuario> signInManager,
            IHttpContextAccessor httpContextAccessor,
            IProdutoService produtoService,
            IFormasPagamentoService formasPagamentoService)
        {
            _loggerService = loggerService;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _signInManager = signInManager;
            _roleManager = RoleManager;
            _userManager = usrMgr;
            _produtoService = produtoService;
            _formasPagamentoService = formasPagamentoService;
        }

        public PedidoService(IProdutoService produtoService, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _produtoService = produtoService;
        }

        public bool AdicionarItemCarrinho(int produtoID, int quantidade, Usuario usuario, ref Pedido pedido)
        {
            if (produtoID <= 0)
                throw new CustomException() { mensagemErro = "Codigo item invalido" };

            var item = _produtoService.BuscarProduto(produtoID);
            if (item == null)
                throw new CustomException() { mensagemErro = "Item não encontrado" };

            ICollection<PedidoItem> Items = new List<PedidoItem>();
            //var ItemContext = _session.GetString($"Pedido-{usuario.Id}");

            if (pedido == null)
            {
                PedidoItem PedidoItem = new PedidoItem();
                //PedidoItem.itempedidoid = Guid.NewGuid();
                PedidoItem.CodigoProduto = item.CodigoProduto;
                PedidoItem.DescricaoProduto = item.Descricao;
                PedidoItem.Quantidade = quantidade;
                PedidoItem.ValorUnitario = item.Valor;
                PedidoItem.ValorTotal = quantidade * item.Valor;

                Items.Add(PedidoItem);
                pedido = GerarPedidoInicial(usuario, Items);

                //_session.SetString($"Pedido-{usuario.Id}", JsonConvert.SerializeObject(pedido));
            }
            else
            {
                //var _pedido = JsonConvert.DeserializeObject<Pedido>(ItemContext);

                if (pedido.PedidoItem != null)
                {
                    Items = pedido.PedidoItem;
                    bool Add = true;
                    foreach (var itemLista in Items)
                    {
                        if (itemLista.CodigoProduto == item.CodigoProduto)
                        {
                            itemLista.DescricaoProduto = item.Descricao;
                            itemLista.Quantidade = quantidade;
                            itemLista.ValorUnitario = item.Valor;
                            itemLista.ValorTotal = quantidade * item.Valor;
                            Add = false;
                        }
                    }

                    if (Add)
                    {
                        PedidoItem PedidoItem = new PedidoItem();
                        //PedidoItem.itempedidoid = Guid.NewGuid();
                        PedidoItem.DescricaoProduto = item.Descricao;
                        PedidoItem.Quantidade = quantidade;
                        PedidoItem.ValorUnitario = item.Valor;
                        PedidoItem.ValorTotal = quantidade * item.Valor;
                        PedidoItem.CodigoPedido = item.CodigoProduto;

                        pedido.PedidoItem.Add(PedidoItem);
                    }

                    //_session.Remove($"Pedido-{usuario.Id}");
                    //_session.SetString($"Pedido-{usuario.Id}", JsonConvert.SerializeObject(_pedido));
                }
                else
                {
                    PedidoItem PedidoItem = new PedidoItem();
                    //PedidoItem.itempedidoid = Guid.NewGuid();
                    PedidoItem.DescricaoProduto = item.Descricao;
                    PedidoItem.Quantidade = quantidade;
                    PedidoItem.ValorUnitario = item.Valor;
                    PedidoItem.ValorTotal = quantidade * item.Valor;
                    //PedidoItem.itemid = item.itemid;
                    //PedidoItem.imagemid = item.imagem[0].imagemid;
                    pedido.PedidoItem.Add(PedidoItem);


                    //_session.Remove($"Pedido-{usuario.Id}");
                    //_session.SetString($"Pedido-{usuario.Id}", JsonConvert.SerializeObject(_pedido));
                }
                
            }
            return true;
        }


        public static Pedido GerarPedidoInicial(Usuario usuario, ICollection<PedidoItem> Pedidoitem = null)
        {
            var dataAtual = DateTime.Now;

            Pedido pedido = new Pedido();
            pedido.CodigoUsuario = usuario.Id;

            pedido.ValorTotal = Pedidoitem.Sum(c => c.ValorTotal);

            pedido.ValorTotalComDesconto = 0;
            pedido.QuatidadeItensVenda = Pedidoitem.Count;
            pedido.DataPedido = dataAtual;
            pedido.DataAprovacaoPedido = DateTime.MinValue;
            pedido.SituacaoPedido = (int)EnumTipo.SituacaoPedido.EmCaptacao;
            pedido.CupomDesconto = string.Empty;
            pedido.FormaPagamentoid = (int)EnumTipo.FormaPagamento.Credito;

            if (Pedidoitem != null && Pedidoitem.Count > 0)
            {
                var _item = new List<PedidoItem>();
                foreach (var item in Pedidoitem)
                {
                    //item.CodigoPedido = pedido.CodigoPedido;
                    _item.Add(item);
                }

                pedido.PedidoItem = _item;

            }

            IList<PedidoHistorico> ListaPedidoHistorico = new List<PedidoHistorico>();
            ListaPedidoHistorico.Add(new PedidoHistorico()
            {
                DataSituacao = dataAtual,
                DataAtualizacaoInicio = dataAtual,
                IdSituacaoPedido = (int)EnumTipo.SituacaoPedido.EmCaptacao,
            });

            pedido.PedidoHistorico = ListaPedidoHistorico;

            return pedido;
        }
    }
}
