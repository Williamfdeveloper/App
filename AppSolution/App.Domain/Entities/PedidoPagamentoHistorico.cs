using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.Domain.Entities
{
    public class PedidoPagamentoHistorico
    {
        public int CodigoPedidoPagamentoHistorico { get; set; }
        public int CodigoPedidoPagamento { get; set; }
        public int IdSituacaoPedidoPagamento { get; set; }
        public DateTime DataAtualizacao { get; set; }

    }
}
