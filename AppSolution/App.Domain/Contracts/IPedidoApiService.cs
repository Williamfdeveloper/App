using App.Domain.Entities;
using App.Domain.Entities.TransacaoPedido;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Contracts
{
    public interface IPedidoApiService
    {
        Pedido GerarPedidoInicial(string token);
        Pedido AdicionarItemPedido(string token, ItemCarrinho ItemCarrinho);
        Pedido AdicionarFormaPagamentoPedido(string token, FomaPagamentoModel FomaPagamentoModel);
        Pedido AdicionarAFilaPedido(string token, FinalizarPedidoModel FinalizarPedidoModel);
        Pedido SolicitarPedidoAtualizado(string token, int codigoPedido);

    }
}
