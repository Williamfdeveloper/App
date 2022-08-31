using App.Domain.Entities.Cartao;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entities.TransacaoPedido
{
    public class FomaPagamentoModel
    {
        public Pedido pedido { get; set; }
        public int codigoFormaPagamento { get; set; }
        

    }
}
