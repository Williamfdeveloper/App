using App.Domain.Contracts;
using App.Domain.Contracts.Adapter;
using App.Domain.Entities;
using App.Domain.Entities.TransacaoPedido;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Service
{
    public class PedidoApiService : IPedidoApiService
    {
        private readonly IPedidoAdapter _pedidoAdapter;

        public PedidoApiService(string token, IPedidoAdapter pedidoAdapter)
        {
            _pedidoAdapter = pedidoAdapter;
        }

        public Pedido AdicionarAFilaPedido(string token, FinalizarPedidoModel FinalizarPedidoModel)
        {
            return _pedidoAdapter.AdicionarAFilaPedido(token, FinalizarPedidoModel);
        }

        public Pedido AdicionarFormaPagamentoPedido(string token, FomaPagamentoModel FomaPagamentoModel)
        {
            return _pedidoAdapter.AdicionarFormaPagamentoPedido(token, FomaPagamentoModel);
        }

        public Pedido AdicionarItemPedido(string token, ItemCarrinho ItemCarrinho)
        {
            return _pedidoAdapter.AdicionarItemPedido(token, ItemCarrinho);
        }

        public Pedido GerarPedidoInicial(string token)
        {
            return _pedidoAdapter.GerarPedidoInicial(token);
        }

        public Pedido SolicitarPedidoAtualizado(string token, int codigoPedido)
        {
            return _pedidoAdapter.SolicitarPedidoAtualizado(token, codigoPedido);
        }
    }
}
