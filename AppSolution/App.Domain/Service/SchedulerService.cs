using App.Domain.Contracts;
using App.Domain.Contracts.Repository;
using App.Domain.Entities;
using App.Domain.Entities.Cartao;
using App.Domain.Entities.TransacaoPedido;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Domain.Service
{
    public class SchedulerService : ISchedulerService
    {
        private readonly IPagamentoService _pagamentoService;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly ILoggerService _loggerService;

        public SchedulerService(IPagamentoService pagamentoService, IPedidoRepository pedidoRepository, ILoggerService loggerService)
        {
            _loggerService = loggerService;
            _pagamentoService = pagamentoService;
            _pedidoRepository = pedidoRepository;
        }

        public Task<bool> ProcessarPedidoFila(CartaoModel cartao, ref Pedido pedido)
        {
            if (cartao == null)
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
    }

}

