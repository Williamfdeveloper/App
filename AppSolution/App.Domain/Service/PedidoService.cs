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
            IProdutoService produtoService,
            IFormasPagamentoService formasPagamentoService,
            IPedidoRepository pedidoRepository,
            IPagamentoService pagamentoService,
            IMessageQueueService messageQueueService)
        {
            _loggerService = loggerService;
            _logger = logger;
            _userManager = usrMgr;
            _produtoService = produtoService;
            _formasPagamentoService = formasPagamentoService;
            _pedidoRepository = pedidoRepository;
            _pagamentoService = pagamentoService;
            _messageQueueService = messageQueueService;
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
            pedido.CodigoFormaPagamento = (int)EnumTipo.FormaPagamento.Credito;

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

            if (FinalizarPedidoModel.pedido.CodigoFormaPagamento != (int)EnumTipo.FormaPagamento.Credito)
                throw new CustomException() { mensagemErro = "Forma de pagamenmto nao autorizada." };


            var pedido = FinalizarPedidoModel.pedido;

            pedido.DataCaptacaoPedido = data;
            pedido.CodigoUsuario = usuario.Id;

            pedido.PedidoHistorico.Add(new PedidoHistorico()
            {
                DataSituacao = data,
                IdSituacaoPedido = (int)EnumTipo.SituacaoPedido.EmCaptacao,
                DataAtualizacaoInicio = data,
                DataAtualizacaoFim = data
            });


            var pedidoPagamento = new PedidoPagamento()
            {
                DataAprovado = data,
                DataAtualizacao = data,
                IdSituacaoPagamento = (int)EnumTipo.SituacaoPedido.EmCaptacao,

            };

            pedidoPagamento.PedidoPagamentoHistorico = new List<PedidoPagamentoHistorico>();
            pedidoPagamento.PedidoPagamentoHistorico.Add(new PedidoPagamentoHistorico()
            {
                DataAtualizacao = data,
                IdSituacaoPedidoPagamento = (int)EnumTipo.SituacaoPedidoPagamento.Iniciada
            });

            pedido.PedidoPagamento = new List<PedidoPagamento>();
            pedido.PedidoPagamento.Add(pedidoPagamento);

            pedido.FormaPagamento = null;
            if (_pedidoRepository.Salvar(ref pedido))
            {
                FinalizarPedidoModel.pedido = pedido;
                return _messageQueueService.PostMessageQueue(FinalizarPedidoModel, (int)EnumTipo.Queue.Pedido);
            }

            return false;
        }

        public Task<bool> ProcessarPedidoFila(CartaoModel cartao, ref Pedido pedido)
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
                return Task.FromResult(true);//Retorna true pra remover da fila

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

                    var pedidoPagamento = new PedidoPagamento()
                    {
                        DataAprovado = dataAtual,
                        DataAtualizacao = dataAtual,
                        IdSituacaoPagamento = (int)EnumTipo.SituacaoPedido.EmCaptacao,

                    };

                    pedidoPagamento.PedidoPagamentoHistorico.Add(new PedidoPagamentoHistorico()
                    {
                        DataAtualizacao = dataAtual,
                        IdSituacaoPedidoPagamento = (int)EnumTipo.SituacaoPedidoPagamento.EmAprovacao
                    });

                    pedido.PedidoPagamento.Add(pedidoPagamento);


                    return Task.FromResult(_pedidoRepository.Atualizar(pedido));
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

                    var pedidoPagamento1 = new PedidoPagamento()
                    {
                        DataAprovado = dataAtual,
                        DataAtualizacao = dataAtual,
                        IdSituacaoPagamento = (int)EnumTipo.SituacaoPedido.EmCaptacao,

                    };

                    pedidoPagamento1.PedidoPagamentoHistorico.Add(new PedidoPagamentoHistorico()
                    {
                        DataAtualizacao = dataAtual,
                        IdSituacaoPedidoPagamento = (int)EnumTipo.SituacaoPedidoPagamento.Aprovado
                    });

                    pedido.PedidoPagamento.Add(pedidoPagamento1);

                    return Task.FromResult(_pedidoRepository.Atualizar(pedido));
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


                    var pedidoPagamento2 = new PedidoPagamento()
                    {
                        DataAprovado = dataAtual,
                        DataAtualizacao = dataAtual,
                        IdSituacaoPagamento = (int)EnumTipo.SituacaoPedido.EmCaptacao,

                    };

                    pedidoPagamento2.PedidoPagamentoHistorico.Add(new PedidoPagamentoHistorico()
                    {
                        DataAtualizacao = dataAtual,
                        IdSituacaoPedidoPagamento = (int)EnumTipo.SituacaoPedidoPagamento.Cancelado
                    });

                    pedido.PedidoPagamento.Add(pedidoPagamento2);
                    
                    return Task.FromResult(_pedidoRepository.Atualizar(pedido));
                default:
                    break;
            }




            throw new NotImplementedException();
        }

        public bool FinalizarPedido(Usuario usuario, CartaoModel cartao, Pedido Pedido)
        {
            throw new NotImplementedException();
        }
    }
}
