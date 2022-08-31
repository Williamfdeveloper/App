using App.Domain.Entities.Cartao;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entities.TransacaoPedido
{
    public class FinalizarPedidoModel
    {
        public Pedido pedido { get; set; }
        public CartaoModel Cartao { get; set; }
    }
}
