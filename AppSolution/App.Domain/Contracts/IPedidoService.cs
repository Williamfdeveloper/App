using App.Domain.Entities;
using App.Domain.Entities.Cartao;
using App.Domain.Entities.TransacaoPedido;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Domain.Contracts
{
    public interface IPedidoService
    {
        Pedido GerarPedidoInicial(Usuario usuario);
        bool AdicionarItemPedido(Usuario usuario, ref ItemCarrinho ItemCarrinho); //int produto, int quantidadeproduto, ref Pedido pedido);
        bool AdicionarFormaPagamentoPedido(Usuario usuario, ref FomaPagamentoModel FomaPagamentoModel);
        bool FinalizarPedido(Usuario usuario, CartaoModel cartao, Pedido Pedido);
        bool AdicionaPedidoFila(Usuario usuario, FinalizarPedidoModel FinalizarPedidoModel);
        Pedido ConsultarPedido(int CodigoPedido);
        Task<bool> ProcessarPedidoFila(CartaoModel cartao, ref Pedido pedido);
    }
}
