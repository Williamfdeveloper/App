using App.Domain.Contracts;
using App.Domain.Contracts.Repository;
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
using System.Transactions;

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
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMessageQueueService _messageQueueService;

        public PedidoService(
            ILoggerService loggerService,
            UserManager<Usuario> usrMgr,
            ILogger<PedidoService> logger,
            RoleManager<IdentityRole> RoleManager,
            SignInManager<Usuario> signInManager,
            IHttpContextAccessor httpContextAccessor,
            IProdutoService produtoService,
            IFormasPagamentoService formasPagamentoService,
            IPedidoRepository pedidoRepository,
            IMessageQueueService messageQueueService)
        {
            _loggerService = loggerService;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _signInManager = signInManager;
            _roleManager = RoleManager;
            _userManager = usrMgr;
            _produtoService = produtoService;
            _formasPagamentoService = formasPagamentoService;
            _pedidoRepository = pedidoRepository;
            _messageQueueService = messageQueueService;
        }

        public PedidoService(IProdutoService produtoService, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _produtoService = produtoService;
        }

        //private static Pedido GerarPedidoInicial(Usuario usuario, ICollection<PedidoItem> Pedidoitem = null)
        //{
        //    var dataAtual = DateTime.Now;

        //    Pedido pedido = new Pedido();
        //    pedido.CodigoUsuario = usuario.Id;

        //    pedido.ValorTotal = Pedidoitem.Sum(c => c.ValorTotal);

        //    pedido.ValorTotalComDesconto = 0;
        //    pedido.QuatidadeItensVenda = Pedidoitem.Count;
        //    pedido.DataPedido = dataAtual;
        //    pedido.DataAprovacaoPedido = DateTime.MinValue;
        //    pedido.SituacaoPedido = (int)EnumTipo.SituacaoPedido.EmCaptacao;
        //    pedido.CupomDesconto = string.Empty;
        //    pedido.FormaPagamentoid = (int)EnumTipo.FormaPagamento.Credito;

        //    if (Pedidoitem != null && Pedidoitem.Count > 0)
        //    {
        //        var _item = new List<PedidoItem>();
        //        foreach (var item in Pedidoitem)
        //        {
        //            //item.CodigoPedido = pedido.CodigoPedido;
        //            _item.Add(item);
        //        }

        //        pedido.PedidoItem = _item;

        //    }

        //    IList<PedidoHistorico> ListaPedidoHistorico = new List<PedidoHistorico>();
        //    ListaPedidoHistorico.Add(new PedidoHistorico()
        //    {
        //        DataSituacao = dataAtual,
        //        DataAtualizacaoInicio = dataAtual,
        //        IdSituacaoPedido = (int)EnumTipo.SituacaoPedido.EmCaptacao,
        //    });

        //    pedido.PedidoHistorico = ListaPedidoHistorico;

        //    return pedido;
        //}

        public Task<bool> CaptarPedido(ref Pedido Pedido)
        {
            DateTime data = DateTime.Now;
            if (Pedido == null)
                throw new CustomException() { mensagemErro = "Objeto pedido não informado." };

            if (Pedido.DataPedido == null)
                throw new CustomException() { mensagemErro = "Data pedido não informado." };

            if (Pedido.ValorTotal == 0)
                throw new CustomException() { mensagemErro = "Valor do pedido não informado." };

            if (string.IsNullOrEmpty(Pedido.CodigoUsuario))
                throw new CustomException() { mensagemErro = "Usuario não informdo no pedido." };

            if (Pedido.FormaPagamento == null)
                throw new CustomException() { mensagemErro = "Codigo da forma de pagamento do pedido não informado." };

            if (Pedido.SituacaoPedido == 0)
                throw new CustomException() { mensagemErro = "Status do pedido invalido." };

            if (Pedido.PedidoItem == null || Pedido.PedidoItem.Count() == 0)
                throw new CustomException() { mensagemErro = "Itens do pedido não informado." };

            if (Pedido.PedidoHistorico == null || Pedido.PedidoHistorico.Count() == 0)
                throw new CustomException() { mensagemErro = "Historico do pedido não informado." };

            if (Pedido.FormaPagamento.CodigoFormaPagamento != (int)EnumTipo.FormaPagamento.Credito)
                throw new CustomException() { mensagemErro = "Forma de pagamenmto nao autorizada." };

            Pedido.DataCaptacaoPedido = data;

            Pedido.PedidoHistorico.Add(new PedidoHistorico()
            {
                DataSituacao = data,
                IdSituacaoPedido = (int)EnumTipo.SituacaoPedido.EmCaptacao,
                DataAtualizacaoInicio = data,
                DataAtualizacaoFim = data
            });

            Pedido.FormaPagamento = null;

            if (_pedidoRepository.Salvar(ref Pedido))
                return _messageQueueService.PostMessageQueue(Pedido, (int)EnumTipo.Queue.Pedido);

            return Task.FromResult(false);
        }

        public Task<Pedido> ConsultarPedido(int CodigoPedido)
        {
            if (CodigoPedido <= 0)
                throw new CustomException() { mensagemErro = "Codigo pedido inválido." };

            return Task.FromResult(_pedidoRepository.Buscar(CodigoPedido));
        }

    }
}
