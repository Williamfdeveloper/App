using App.Domain.Contracts;
using App.Domain.Contracts.Repository;
using App.Domain.Entities;
using App.Domain.Entities.Cartao;
using App.Domain.Entities.TransacaoPedido;
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
        private readonly IPagamentoService _pagamentoService;
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
            IPagamentoService pagamentoService,
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
            _pagamentoService = pagamentoService;
            _messageQueueService = messageQueueService;
        }

        public PedidoService(IProdutoService produtoService, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _produtoService = produtoService;
        }

        public Pedido GerarPedidoInicial(Usuario usuario)
        {
            var dataAtual = DateTime.Now;

            Pedido pedido = new Pedido();
            pedido.CodigoUsuario = usuario.Id;
            pedido.ValorTotalComDesconto = 0;
            pedido.DataPedido = dataAtual;
            pedido.DataAprovacaoPedido = DateTime.MinValue;
            pedido.SituacaoPedido = (int)EnumTipo.SituacaoPedido.EmCaptacao;
            pedido.CupomDesconto = string.Empty;
            pedido.FormaPagamentoid = (int)EnumTipo.FormaPagamento.Credito;

            pedido.PedidoHistorico = new List<PedidoHistorico>();
            pedido.PedidoHistorico.Add(new PedidoHistorico()
            {
                DataSituacao = dataAtual,
                DataAtualizacaoInicio = dataAtual,
                IdSituacaoPedido = (int)EnumTipo.SituacaoPedido.EmCaptacao,
            });

            pedido.PedidoItem = new List<PedidoItem>();

            return pedido;
        }
        public Pedido ConsultarPedido(int CodigoPedido)
        {
            if (CodigoPedido <= 0)
                throw new CustomException() { mensagemErro = "Codigo pedido inválido." };

            return _pedidoRepository.Buscar(CodigoPedido);
        }
        public bool AdicionarItemPedido(Usuario usuario, ref ItemCarrinho ItemCarrinho)
        {
            if (usuario == null)
                throw new CustomException() { mensagemErro = "Usuario não informado." };

            if (ItemCarrinho.codigoProduto < 0)
                throw new CustomException() { mensagemErro = "Codigo Produto Invalido" };

            if (ItemCarrinho.pedido == null)
                throw new CustomException() { mensagemErro = "Pedido não informado" };

            var produto = _produtoService.BuscarProduto(ItemCarrinho.codigoProduto);
            if (produto == null)
                throw new CustomException() { mensagemErro = "Produto Não encontrado" };

            if (ItemCarrinho.pedido.PedidoItem == null)
                ItemCarrinho.pedido.PedidoItem = new List<PedidoItem>();

            ItemCarrinho.pedido.PedidoItem.Add(new PedidoItem()
            {
                CodigoProduto = produto.CodigoProduto,
                DescricaoProduto = produto.Descricao,
                Quantidade = ItemCarrinho.quantidade,
                ValorUnitario = produto.Valor,
                ValorTotal = ItemCarrinho.quantidade * produto.Valor
            });

            ItemCarrinho.pedido.ValorTotal = ItemCarrinho.pedido.PedidoItem.Sum(c => c.ValorTotal);
            ItemCarrinho.pedido.ValorTotalComDesconto = ItemCarrinho.pedido.PedidoItem.Sum(c => c.ValorTotal);
            ItemCarrinho.pedido.QuatidadeItensVenda = ItemCarrinho.pedido.PedidoItem.Count;

            return true;
        }
        public bool AdicionarFormaPagamentoPedido(Usuario usuario, ref FomaPagamentoModel FomaPagamentoModel)
        {
            if (usuario == null)
                throw new CustomException() { mensagemErro = "Usuario não informado." };

            //Adicionar parametro de sistema para esta validação
            if (FomaPagamentoModel.codigoFormaPagamento < 0)
                throw new CustomException() { mensagemErro = "Codigo Produto Invalido" };

            if (FomaPagamentoModel.pedido == null)
                throw new CustomException() { mensagemErro = "Pedido não informado" };

            var forma = _formasPagamentoService.Buscar(FomaPagamentoModel.codigoFormaPagamento);
            if (forma == null)
                throw new CustomException() { mensagemErro = "Forma de pagamento não encontrado" };

            FomaPagamentoModel.pedido.FormaPagamento = forma;

            return true;
        }
        public bool FinalizarPedido(Usuario usuario, CartaoModel cartao, Pedido Pedido)
        {
            DateTime data = DateTime.Now;
            if (usuario == null)
                throw new CustomException() { mensagemErro = "Usuario não informado." };

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
            Pedido.CodigoUsuario = usuario.Id;

            Pedido.PedidoHistorico.Add(new PedidoHistorico()
            {
                CodigoPedido = Pedido.CodigoPedido,
                DataSituacao = data,
                IdSituacaoPedido = (int)EnumTipo.SituacaoPedido.EmCaptacao,
                DataAtualizacaoInicio = data,
                DataAtualizacaoFim = data
            });

            foreach (var item in Pedido.PedidoItem)
            {
                item.CodigoPedido = Pedido.CodigoPedido;
            }

            Pedido.PedidoPagamento.CodigoPedido = Pedido.CodigoPedido;
            Pedido.PedidoPagamento.PedidoPagamentoHistorico.Add(new PedidoPagamentoHistorico()
            {
                CodigoPedidoPagamento = Pedido.PedidoPagamento.CodigoPedidoPagamento,
                DataAtualizacao = data,
                IdSituacaoPedidoPagamento = (int)EnumTipo.SituacaoPedidoPagamento.Iniciada
            });

            var retornoPagamento = _pagamentoService.ProcessarPagamento(cartao, Pedido.ValorTotalComDesconto);
            switch (retornoPagamento)
            {
                case (int)EnumTipo.SituacaoPedidoPagamento.EmAprovacao:
                    break;
                case (int)EnumTipo.SituacaoPedidoPagamento.Aprovado:
                    break;
                case (int)EnumTipo.SituacaoPedidoPagamento.Cancelado:
                    break;
                default:
                    break;
            }

            Pedido.FormaPagamento = null;

            if (_pedidoRepository.Salvar(ref Pedido))
                return _messageQueueService.PostMessageQueue(Pedido, (int)EnumTipo.Queue.Pedido);

            return false;
        }
        public bool AdicionaPedidoFila(Usuario usuario, FinalizarPedidoModel FinalizarPedidoModel)
        {
            DateTime data = DateTime.Now;
            if (usuario == null)
                throw new CustomException() { mensagemErro = "Usuario não informado." };

            if (FinalizarPedidoModel.pedido == null)
                throw new CustomException() { mensagemErro = "Objeto pedido não informado." };

            if (FinalizarPedidoModel.pedido.DataPedido == null)
                throw new CustomException() { mensagemErro = "Data pedido não informado." };

            if (FinalizarPedidoModel.pedido.ValorTotal == 0)
                throw new CustomException() { mensagemErro = "Valor do pedido não informado." };

            if (string.IsNullOrEmpty(FinalizarPedidoModel.pedido.CodigoUsuario))
                throw new CustomException() { mensagemErro = "Usuario não informdo no pedido." };

            if (FinalizarPedidoModel.pedido.FormaPagamento == null)
                throw new CustomException() { mensagemErro = "Codigo da forma de pagamento do pedido não informado." };

            if (FinalizarPedidoModel.pedido.SituacaoPedido == 0)
                throw new CustomException() { mensagemErro = "Status do pedido invalido." };

            if (FinalizarPedidoModel.pedido.PedidoItem == null || FinalizarPedidoModel.pedido.PedidoItem.Count() == 0)
                throw new CustomException() { mensagemErro = "Itens do pedido não informado." };

            if (FinalizarPedidoModel.pedido.PedidoHistorico == null || FinalizarPedidoModel.pedido.PedidoHistorico.Count() == 0)
                throw new CustomException() { mensagemErro = "Historico do pedido não informado." };

            if (FinalizarPedidoModel.pedido.FormaPagamento.CodigoFormaPagamento != (int)EnumTipo.FormaPagamento.Credito)
                throw new CustomException() { mensagemErro = "Forma de pagamenmto nao autorizada." };


            var pedido = FinalizarPedidoModel.pedido;


            pedido.DataCaptacaoPedido = data;
            pedido.CodigoUsuario = usuario.Id;

            pedido.PedidoHistorico.Add(new PedidoHistorico()
            {
                //CodigoPedido = FinalizarPedidoModel.pedido.CodigoPedido,
                DataSituacao = data,
                IdSituacaoPedido = (int)EnumTipo.SituacaoPedido.EmCaptacao,
                DataAtualizacaoInicio = data,
                DataAtualizacaoFim = data
            });

            foreach (var item in pedido.PedidoItem)
            {
                item.CodigoPedido = FinalizarPedidoModel.pedido.CodigoPedido;
            }

            pedido.PedidoPagamento.CodigoPedido = FinalizarPedidoModel.pedido.CodigoPedido;
            pedido.PedidoPagamento.PedidoPagamentoHistorico.Add(new PedidoPagamentoHistorico()
            {
                CodigoPedidoPagamento = pedido.PedidoPagamento.CodigoPedidoPagamento,
                DataAtualizacao = data,
                IdSituacaoPedidoPagamento = (int)EnumTipo.SituacaoPedidoPagamento.Iniciada
            });

            //var retornoPagamento = _pagamentoService.EfetuarPagamento(cartao, Pedido.ValorTotalComDesconto);

            //switch (retornoPagamento)
            //{
            //    case (int)EnumTipo.SituacaoPedidoPagamento.EmAprovacao:
            //        break;
            //    case (int)EnumTipo.SituacaoPedidoPagamento.Aprovado:
            //        break;
            //    case (int)EnumTipo.SituacaoPedidoPagamento.Cancelado:
            //        break;
            //    default:
            //        break;
            //}

            pedido.FormaPagamento = null;
            if (_pedidoRepository.Salvar(ref pedido))
            {
                FinalizarPedidoModel.pedido = pedido;
                return _messageQueueService.PostMessageQueue(FinalizarPedidoModel, (int)EnumTipo.Queue.Pedido);
            }

            return false;
        }

        public bool ProcessarPedidoFila(CartaoModel cartao, ref Pedido pedido)
        {
            if (cartao != null)
                throw new CustomException() { mensagemErro = "Objeto Pedido não informado." };

            if (pedido == null)
                throw new CustomException() { mensagemErro = "Objeto pedido não informado." };

            if (pedido.DataPedido == null)
                throw new CustomException() { mensagemErro = "Data pedido não informado." };

            if (pedido.ValorTotal == 0)
                throw new CustomException() { mensagemErro = "Valor do pedido não informado." };

            if (string.IsNullOrEmpty(pedido.CodigoUsuario))
                throw new CustomException() { mensagemErro = "Usuario não informdo no pedido." };

            if (pedido.FormaPagamento == null)
                throw new CustomException() { mensagemErro = "Codigo da forma de pagamento do pedido não informado." };

            if (pedido.SituacaoPedido == 0)
                throw new CustomException() { mensagemErro = "Status do pedido invalido." };

            if (pedido.PedidoItem == null || pedido.PedidoItem.Count() == 0)
                throw new CustomException() { mensagemErro = "Itens do pedido não informado." };

            if (pedido.PedidoHistorico == null || pedido.PedidoHistorico.Count() == 0)
                throw new CustomException() { mensagemErro = "Historico do pedido não informado." };

            if (pedido.FormaPagamento.CodigoFormaPagamento != (int)EnumTipo.FormaPagamento.Credito)
                throw new CustomException() { mensagemErro = "Forma de pagamenmto nao autorizada." };

            var usuario = _userManager.FindByIdAsync(pedido.CodigoUsuario).Result;
            if (usuario == null)
                throw new CustomException() { mensagemErro = "Usuario não identificado, por gentileza, efetue o Login." };

            if (pedido.SituacaoPedido == (int)EnumTipo.SituacaoPedido.EmAprovacao || pedido.SituacaoPedido == (int)EnumTipo.SituacaoPedido.Cancelado)
                return true;//Retorna true pra remover da fila

            var retornopagamento = _pagamentoService.ProcessarPagamento(cartao, pedido.ValorTotalComDesconto);
            var dataAtual = DateTime.Now;
            switch (retornopagamento)
            {
                case (int)EnumTipo.SituacaoPedidoPagamento.EmAprovacao:
                    pedido.SituacaoPedido = (int)EnumTipo.SituacaoPedido.EmAprovacao;
                    pedido.DataAtualizacaoPedido = dataAtual;

                    pedido.PedidoHistorico.Where(c => c.CodigoPedido.Equals(c.CodigoPedido) && c.DataAtualizacaoFim.Equals(null)).Select(c =>
                    {
                        c.DataAtualizacaoFim = dataAtual;
                        return c;
                    }).OrderByDescending(c => c.DataSituacao).ToList();

                    pedido.PedidoHistorico.Add(new PedidoHistorico()
                    {
                        CodigoPedido = pedido.CodigoPedido,
                        DataAtualizacaoInicio = dataAtual,
                        DataSituacao = dataAtual,
                        IdSituacaoPedido = (int)EnumTipo.SituacaoPedido.EmAprovacao,
                    });

                    pedido.PedidoPagamento.PedidoPagamentoHistorico.Add(new PedidoPagamentoHistorico()
                    {
                        CodigoPedidoPagamento = pedido.PedidoPagamento.CodigoPedidoPagamento,
                        DataAtualizacao = dataAtual,
                        IdSituacaoPedidoPagamento = (int)EnumTipo.SituacaoPedidoPagamento.EmAprovacao
                    });

                    return _pedidoRepository.Atualizar(pedido);
                case (int)EnumTipo.SituacaoPedidoPagamento.Aprovado:
                    pedido.SituacaoPedido = (int)EnumTipo.SituacaoPedido.Finalizado;
                    pedido.DataAtualizacaoPedido = dataAtual;

                    pedido.PedidoHistorico.Where(c => c.CodigoPedido.Equals(c.CodigoPedido) && c.DataAtualizacaoFim.Equals(null)).Select(c =>
                    {
                        c.DataAtualizacaoFim = dataAtual;
                        return c;
                    }).OrderByDescending(c => c.DataSituacao).ToList();

                    pedido.PedidoHistorico.Add(new PedidoHistorico()
                    {
                        CodigoPedido = pedido.CodigoPedido,
                        DataAtualizacaoInicio = dataAtual,
                        DataSituacao = dataAtual,
                        IdSituacaoPedido = (int)EnumTipo.SituacaoPedido.Finalizado,
                    });

                    pedido.PedidoPagamento.PedidoPagamentoHistorico.Add(new PedidoPagamentoHistorico()
                    {
                        CodigoPedidoPagamento = pedido.PedidoPagamento.CodigoPedidoPagamento,
                        DataAtualizacao = dataAtual,
                        IdSituacaoPedidoPagamento = (int)EnumTipo.SituacaoPedidoPagamento.Aprovado

                    });

                    return _pedidoRepository.Atualizar(pedido);
                case (int)EnumTipo.SituacaoPedidoPagamento.Cancelado:
                    pedido.SituacaoPedido = (int)EnumTipo.SituacaoPedido.Cancelado;
                    pedido.DataAtualizacaoPedido = dataAtual;

                    pedido.PedidoHistorico.Where(c => c.CodigoPedido.Equals(c.CodigoPedido) && c.DataAtualizacaoFim.Equals(null)).Select(c =>
                    {
                        c.DataAtualizacaoFim = dataAtual;
                        return c;
                    }).OrderByDescending(c => c.DataSituacao).ToList();

                    pedido.PedidoHistorico.Add(new PedidoHistorico()
                    {
                        CodigoPedido = pedido.CodigoPedido,
                        DataAtualizacaoInicio = dataAtual,
                        DataSituacao = dataAtual,
                        IdSituacaoPedido = (int)EnumTipo.SituacaoPedido.Cancelado,
                    });

                    pedido.PedidoPagamento.PedidoPagamentoHistorico.Add(new PedidoPagamentoHistorico()
                    {
                        CodigoPedidoPagamento = pedido.PedidoPagamento.CodigoPedidoPagamento,
                        DataAtualizacao = dataAtual,
                        IdSituacaoPedidoPagamento = (int)EnumTipo.SituacaoPedidoPagamento.Cancelado

                    });

                    return _pedidoRepository.Atualizar(pedido);
                default:
                    break;
            }




            throw new NotImplementedException();
        }
    }
}
